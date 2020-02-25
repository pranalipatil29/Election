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
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        public async Task<bool> Register(UserRegitration registrationModel)
        {
            // check wheather admin is already preent in database or not
            var user = await this.userManager.FindByEmailAsync(registrationModel.EmailID);

            // check wheather admin record i found or not
            if (user == null)
            {
                var userData = this.authenticationContext.AccountTable.Where(s => s.MobileNumber == registrationModel.MobileNumber).FirstOrDefault();

                if (userData == null)
                {
                    // get the admin data
                    var data = new ApplicationModel()
                    {
                        FirstName = registrationModel.FirstName,
                        LastName = registrationModel.LastName,
                        Email = registrationModel.EmailID,
                        UserName = registrationModel.UserName,
                        MobileNumber = registrationModel.MobileNumber,
                        ProfilePicture = registrationModel.ProfilePicture,
                        Vote = 0,
                        UserType = "User"
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
                    throw new Exception("Record already exist");
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
        public async Task<UserResponse> Login(UserLogin loginModel)
        {
            try
            {
                // check whether the admin entered email id is present in datbase
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailID);

                // check whether the admin entered password is matching
                var userPassword = await this.userManager.CheckPasswordAsync(user, loginModel.Password);

                // check whether admin data is null or not or admin password is correct 
                if (user != null && userPassword && user.UserType == "User")
                {
                    // get the required admin data 
                    var data = new UserResponse()
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
        public async Task<string> GenerateToken(UserResponse accountResponse)
        {
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
        public async Task<UserResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // check whether user data exist in the database or not
                var user = await this.userManager.FindByEmailAsync(emailID);

                // check wheather user data contains any null value or not
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
                    var data = new UserResponse()
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
        /// Gives the vote.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
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
        public async Task<bool> GiveVote(string emailID, VoteRequest voteRequest)
        {
            try
            {
                var user = this.authenticationContext.AccountTable.Where(s => s.Email == emailID).FirstOrDefault();

                if (user != null)
                {
                    if (user.Vote == 1)
                    {
                        throw new Exception("Your vote is already submitted");
                    }
                    else
                    {
                        var candidate = this.authenticationContext.Candidates.Where(s => s.CandidateID == voteRequest.CandidateID && s.ConstituencyID == voteRequest.ConstatuencyID).FirstOrDefault();

                        if (candidate != null)
                        { 
                           user.Vote = 1;
                           this.authenticationContext.AccountTable.Update(user);

                            var resultData = this.authenticationContext.Result.Where(s => s.CandidateID == voteRequest.CandidateID).FirstOrDefault();

                            if (resultData != null)
                            {
                                resultData.Votes = resultData.Votes + 100;
                                this.authenticationContext.Result.Update(resultData);
                            }
                            else
                            {
                                var result = new ResultModel()
                                {
                                    CandidateID = voteRequest.CandidateID,
                                    Votes = 100
                                };

                                this.authenticationContext.Result.Add(result);
                            }                           

                           await this.authenticationContext.SaveChangesAsync();
                           return true;
                        }
                        else
                        {
                           throw new Exception("Cadidate Not found");
                        }
                    }                       
                }
                else
                {
                    throw new Exception("User Not found");
                }                                 
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
