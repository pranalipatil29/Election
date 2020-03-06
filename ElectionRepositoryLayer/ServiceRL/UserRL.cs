using ElectionCommonLayer.Model;
using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using ElectionCommonLayer.Model.Vote;
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

namespace ElectionRepositoryLayer.ServiceRL
{
    public class UserRL : IUserRL
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
        public UserRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings, AuthenticationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSettings = appSettings.Value;
            this.authenticationContext = context;
        }

        /// <summary>
        /// Gives the vote.
        /// </summary>
        /// <param name="voteRequest">The vote request.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Your vote is already submitted
        /// or
        /// Cadidate Not found
        /// or
        /// Please select Valid Constituency Details
        /// or
        /// User Not found
        /// or
        /// </exception>
        public async Task<bool> GiveVote(VoteRequest voteRequest)
        {
            try
            {
                var user = this.authenticationContext.User.Where(s => s.MobileNumber == voteRequest.MobileNumber).FirstOrDefault();

                if (user == null)
                {
                    var data = new UserModel()
                    {
                        MobileNumber = voteRequest.MobileNumber,
                        Vote = 0,
                        FirstName = "User",
                        LastName = "User"
                    };

                    this.authenticationContext.User.Add(data);
                    await this.authenticationContext.SaveChangesAsync();

                    var result = await SubmitVote(data.UserID, voteRequest);

                    return result;
                }

                if (user != null)
                {
                    if (user.Vote == 1)
                    {
                        throw new Exception("Your vote is already submitted");
                    }
                    else
                    {
                        var result = await SubmitVote(user.UserID, voteRequest);

                        return result;
                    }
                }
                else
                {
                    throw new Exception("User Not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// this method is used to submit the vote
        /// </summary>
        /// <param name="userID"> the user Identifier</param>
        /// <param name="voteRequest"> vote request values</param>
        /// <returns>returns true or false indicating operation result</returns>
        private async Task<bool> SubmitVote(int userID, VoteRequest voteRequest)
        {
            try
            {
                // find the user in user table
                var user = this.authenticationContext.User.Where(s => s.UserID == userID).FirstOrDefault();

                // check wheather uer is found or not from uer table
                if (user != null)
                {
                    // check wheather user entered valid info 
                    if (voteRequest.CandidateID > 0 && voteRequest.ConstituencyID > 0 && voteRequest.StateID > 0)
                    {
                        // find the user entered state from states table
                        var state = this.authenticationContext.States.Where(s => s.StateID == voteRequest.StateID).FirstOrDefault();

                        if (state != null)
                        {
                            // find the candidate from candidate table
                            var candidate = this.authenticationContext.Candidates.Where(s => s.CandidateID == voteRequest.CandidateID && s.ConstituencyID == voteRequest.ConstituencyID && s.StateID == voteRequest.StateID).FirstOrDefault();

                            // check wheather candidate record found or not
                            if (candidate != null)
                            {
                                // mark user voted as true
                                user.Vote = 1;

                                // save the changes into user table
                                this.authenticationContext.User.Update(user);
                                await this.authenticationContext.SaveChangesAsync();

                                // get the result for selected candidate from result table
                                var resultData = this.authenticationContext.Result.Where(s => s.CandidateID == voteRequest.CandidateID).FirstOrDefault();

                                // check wheather any record found from result table or not
                                if (resultData != null)
                                {
                                    // if record found then add the votes value for selected candidate
                                    resultData.Votes = resultData.Votes + 100;

                                    // save the changes in result table
                                    this.authenticationContext.Result.Update(resultData);
                                    await this.authenticationContext.SaveChangesAsync();
                                }
                                else
                                {
                                    // if candidate record not found in result table then create the record for candidate 
                                    var result = new ResultModel()
                                    {
                                        CandidateID = voteRequest.CandidateID,
                                        Votes = 100
                                    };

                                    // adding the candidate record into result table
                                    this.authenticationContext.Result.Add(result);
                                    await this.authenticationContext.SaveChangesAsync();
                                }

                                // return true value indicating operation is successful
                                return true;
                            }
                            else
                            {
                                throw new Exception("Cadidate Not found");
                            }
                        }
                        else
                        {
                            throw new Exception("State record not found");
                        } 
                    }
                    else
                    {
                        throw new Exception("Please enter correct state, constituency & candidate Id's");
                    }
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }    
}