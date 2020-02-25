﻿// ******************************************************************************
//  <copyright file="AdminRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminRL.cs
//  
//     Purpose: Implementing Login, registration, Change Profile functionality for admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Admin.Request;
    using ElectionCommonLayer.Model.Admin.Respone;
    using ElectionCommonLayer.Model.Candidate;
    using ElectionCommonLayer.Model.Party;
    using ElectionCommonLayer.Model.Result;
    using ElectionRepositoryLayer.Context;
    using ElectionRepositoryLayer.ImageUpload;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this class contains different methods to interact with database
    /// </summary>
    /// <seealso cref="ElectionRepositoryLayer.InterfaceRL.IAdminRL" />
    public class AdminRL : IAdminRL
    {
        /// <summary>
        /// The user manager reference
        /// </summary>
        private readonly UserManager<ApplicationModel> userManager;

        /// <summary>
        /// The sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationModel> signInManager;

        /// <summary>
        /// The application settings
        /// </summary>
        private readonly ApplicationSetting applicationSettings;

        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRL"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="appSettings">The application settings.</param>
        public AdminRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings, AuthenticationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSettings = appSettings.Value;
            this.authenticationContext = context;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            // check wheather admin is already preent in database or not
            var user = await this.userManager.FindByEmailAsync(registrationModel.EmailID);

            // check wheather admin record i found or not
            if (user == null)
            {
                // get the admin data
                var data = new ApplicationModel()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Email = registrationModel.EmailID,
                    UserName = registrationModel.UserName,
                    MobileNumber = registrationModel.MobileNumber,
                    Vote = 0,
                    ProfilePicture = registrationModel.ProfilePicture,
                    UserType = "Admin"
                };

                // create record in admin table
                var result = await this.userManager.CreateAsync(data, registrationModel.Password);

                // check wheather operation isuccesfully completed or not
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> return the admin details or null value</returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<AccountResponse> Login(LogInModel loginModel)
        {
            try
            {
                // check whether the admin entered email id is present in datbase
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailID);

                // check whether the admin entered password is matching
                var userPassword = await this.userManager.CheckPasswordAsync(user, loginModel.Password);

                // check whether admin data is null or not or admin password is correct 
                if (user != null && userPassword && user.UserType == "Admin")
                {
                    // get the required admin data 
                    var data = new AccountResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        UserName = user.UserName,
                        ProfilePicture = user.ProfilePicture,
                    };

                    // return the admin data
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns>
        /// returns token or null value
        /// </returns>
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            // check whether admin data exist in the database or not
            var user = await this.userManager.FindByEmailAsync(accountResponse.EmailID);

            // check whether user email id and password is found or not 
            if (user != null)
            {
                // creating token for specific email id
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("EmailID", user.Email.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the admin details or null value</returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // check whether admin data exist in the database or not
                var user = await this.userManager.FindByEmailAsync(emailID);

                // check wheather admin data contains any null value or not
                if (user != null)
                {
                    // send the API key,API secret key and cloud name to Upload Image class constructor
                    UploadImage imageUpload = new UploadImage(this.applicationSettings.APIkey, this.applicationSettings.APISecret, this.applicationSettings.CloudName);

                    // get the image url
                    var url = imageUpload.Upload(formFile);

                    // set the profile picture
                    user.ProfilePicture = url;
                    var result = await this.userManager.UpdateAsync(user);

                    // get the required user data 
                    var data = new AccountResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        UserName = user.UserName,
                        ProfilePicture = user.ProfilePicture,
                    };

                    // returning admin data
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// returns result or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Authentication Problem
        /// or
        /// </exception>
        public IList<ResultResponse> GetResult(string adminID)
        {
            try
            {
                // check whether admin data exist in the database or not
                var admin = this.authenticationContext.AccountTable.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin data contains any null value or not
                if (admin != null)
                {
                    // get the all data from result table
                    var results = this.authenticationContext.Result.Select(s => s);

                    // check wheather results contain any dataor not
                    if (results != null)
                    {
                        // creating list type variable to store results
                        var list = new List<ResultResponse>();

                        // iterates the list for all results
                        foreach (var result in results)
                        {
                            // get the candidate details from candidate table
                            var candidateData = this.authenticationContext.Candidates.Where(s => s.CandidateID == result.CandidateID).FirstOrDefault();

                            // get the all data to return result
                            var data = new ResultResponse()
                            {
                                CandidateID = result.CandidateID,
                                CandidateName = candidateData.CandidateName,
                                PartyID = candidateData.PartyID,
                                PartyName = candidateData.PartyName,
                                State = candidateData.State,
                                ConstituencyName = candidateData.ConstituencyName,
                                Votes = result.Votes
                            };

                            // add data into list
                            list.Add(data);
                        }

                        // return the list
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Authentication Problem");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Costituencywises the ressult.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// returns Constituency wise result or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Authentication Problem
        /// or
        /// </exception>
        public IList<ResultResponse> CostituencywiseRessult(string adminID, int constituencyID, string state)
        {
            try
            {
                // verify the admin
                var admin = this.authenticationContext.AccountTable.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin data contains any null value or not
                if (admin != null)
                {
                    // get the result 
                    var results = GetResult(adminID);

                    // check wheather the result contains any record or not
                    if (results.Count > 0)
                    {
                        // creating variable to store list of response
                        var list = new List<ResultResponse>();

                        // iterates the loop for each result
                        foreach (var result in results)
                        {
                            // get the cadidates info for required constituency and state
                            var resultData = this.authenticationContext.Candidates.Where(s => s.CandidateID == result.CandidateID && s.State == state && s.ConstituencyID == constituencyID);

                            // iterates the loop for each result data
                            foreach (var candidate in resultData)
                            {
                                // get the data in responsse format
                                var data = new ResultResponse()
                                {
                                    CandidateID = result.CandidateID,
                                    CandidateName = candidate.CandidateName,
                                    PartyID = candidate.PartyID,
                                    PartyName = candidate.PartyName,
                                    State = candidate.State,
                                    ConstituencyName = candidate.ConstituencyName,
                                    Votes = result.Votes
                                };

                                // add the data into list
                                list.Add(data);
                            }
                        }

                        // return list
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Authentication Problem");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IList<PartywiseResultResponse> PartywiseResult(string adminID, string state)
        {
            try
            {
                // verify the admin
                var admin = this.authenticationContext.AccountTable.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin data contains any null value or not
                if (admin != null)
                {
                    // creating list object to hold the count of candidates for parties
                    var candidateCountforParty = new List<CandidatesCountForParty>();

                    // creating object to hold parties response
                    var partiesList = new List<PartyResponse>();

                    // creating object to hold winner details of each constituency
                    var candidateResultByConstituency = new List<CandidateResultByConstituency>();

                    // creating object to hold final Election Result
                    var finalResult = new List<PartywiseResultResponse>();

                    // get the constituencies in required state
                    var constituencies = this.authenticationContext.Constituencies.Where(s => s.State == state);

                    // get the the parties info
                    var parties = this.authenticationContext.Parties.Select(s => s);

                    // get the candidate details in paerticular state
                    var statewiseCandidates = this.authenticationContext.Candidates.Where(s => s.State == state);

                    // this variable i used to count the candidates in each party
                    int count = 0;
                                      
                    if (statewiseCandidates.Count() > 0)
                    {
                        if (parties != null)
                        {
                            // get the Candidates ID for particular Party
                            var ids = new List<int>();

                            // iterate the loop for each party
                            foreach (var party in parties)
                            {
                                // iterates the loop for each candidate in state
                                foreach (var candidate in statewiseCandidates)
                                {
                                    // check the candidate party
                                    if (party.PartyID == candidate.PartyID)
                                    {
                                        count = count + 1;
                                        ids.Add(candidate.CandidateID);
                                    }
                                }

                                // get the party details and candidate count for that party
                                var data = new CandidatesCountForParty()
                                {
                                    PartyID = party.PartyID,
                                    Count = count,
                                    PartyName = party.PartyName
                                };

                                // add the details of party into list
                                candidateCountforParty.Add(data);
                                count = 0;
                            }
                        }
                        else
                        {
                            throw new Exception("Party Record not found");
                        }

                        // check wheather sconstituencies varaiable contains any null value or not
                        if (constituencies != null)
                        {
                            // iterates the loop for each Contituency record
                            foreach (var constituency in constituencies)
                            {
                                // this list is used to holds the result of each constituency
                                var list = new List<ResultResponse>();

                                // get the candidates for particular Constituency
                                var candidates = this.authenticationContext.Candidates.Where(s => s.ConstituencyID == constituency.ConstituencyID && s.State == state);

                                // check wheather candidates for particular constituency are found or not
                                if (candidates != null)
                                {
                                    // iterate the loop for each candidate
                                    foreach (var candidate in candidates)
                                    {
                                        // get the vote count for particular candidate
                                        var candidateVotes = this.authenticationContext.Result.Where(s => s.CandidateID == candidate.CandidateID).FirstOrDefault();

                                        if (candidateVotes != null)
                                        {
                                            var data = new ResultResponse()
                                            {
                                                CandidateID = candidate.CandidateID,
                                                CandidateName = candidate.CandidateName,
                                                PartyID = candidate.PartyID,
                                                PartyName = candidate.PartyName,
                                                Votes = candidateVotes.Votes,
                                                ConstituencyName = candidate.ConstituencyName,
                                                State = candidate.State
                                            };

                                            list.Add(data);
                                        }
                                        else
                                        {
                                            throw new Exception("Elections are not Done...!");
                                        }
                                    }

                                    // sort the candidates accourding to votes
                                    for (int i = 0; i < list.Count - 1; i++)
                                    {
                                        for (int j = i + 1; j < list.Count; j++)
                                        {
                                            if (list[i].Votes < list[j].Votes)
                                            {
                                                var temp = list[i];
                                                list[i] = list[j];
                                                list[j] = temp;
                                            }
                                        }
                                    }
                                }

                                // get the winner details of paerticular constituency
                                var resultByConstituency = new CandidateResultByConstituency()
                                {
                                    ConstituencyID = constituency.ConstituencyID,
                                    CandidtesResultByContituency = list[0]
                                };

                                // add the winner details in list
                                candidateResultByConstituency.Add(resultByConstituency);
                            }

                            // iterates the loop for each party
                            foreach (var party in parties)
                            {
                                // won variable is used to indicate how many candidates of perticular party won
                                int won = 0;

                                // loss variable is used to indicate how many candidates of perticular party loss
                                int loss = 0;

                                // iterates the loop for each record of candidateResultByConstituency to calculate winner
                                foreach (var result in candidateResultByConstituency)
                                {
                                    // check the party details
                                    if (party.PartyID == result.CandidtesResultByContituency.PartyID)
                                    {
                                        won = won + 1;
                                    }
                                }

                                // iterates the loop for each record of candidateResultByConstituency to calculate loss
                                foreach (var candidatesCount in candidateCountforParty)
                                {
                                    if (candidatesCount.PartyID == party.PartyID)
                                    {
                                        loss = candidatesCount.Count - won;
                                    }
                                }

                                var partywiseReult = new PartywiseResultResponse()
                                {
                                    PartyID = party.PartyID,
                                    PartyName = party.PartyName,
                                    Won = won,
                                    Loss = loss
                                };

                                if (partywiseReult.Loss != 0 || partywiseReult.Won != 0)
                                {
                                    finalResult.Add(partywiseReult);
                                }
                            }

                            return finalResult;
                        }
                        else
                        {
                            throw new Exception("Constitutes for selected State not found");
                        }
                    }
                    else
                    {
                        throw new Exception("Candidate Records not found");
                    }
                }
                else
                {
                    throw new Exception("Authentication Problem");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}