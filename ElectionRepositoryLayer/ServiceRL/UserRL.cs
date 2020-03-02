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

        private async Task<bool> SubmitVote(int userID, VoteRequest voteRequest)
        {
            try
            {
                var user = this.authenticationContext.User.Where(s => s.UserID == userID).FirstOrDefault();

                if (user != null)
                {
                    if (voteRequest.CandidateID > 0 && voteRequest.ConstatuencyID > 0)
                    {
                        var candidate =  this.authenticationContext.Candidates.Where(s => s.CandidateID == voteRequest.CandidateID && s.ConstituencyID == voteRequest.ConstatuencyID).FirstOrDefault();

                        if (candidate != null)
                        {
                            user.Vote = 1;
                            this.authenticationContext.User.Update(user);
                            await this.authenticationContext.SaveChangesAsync();

                            var resultData = this.authenticationContext.Result.Where(s => s.CandidateID == voteRequest.CandidateID).FirstOrDefault();

                            if (resultData != null)
                            {
                                resultData.Votes = resultData.Votes + 100;
                                this.authenticationContext.Result.Update(resultData);
                                await this.authenticationContext.SaveChangesAsync();
                            }
                            else
                            {
                                var result = new ResultModel()
                                {
                                    CandidateID = voteRequest.CandidateID,
                                    Votes = 100
                                };

                                this.authenticationContext.Result.Add(result);
                                await this.authenticationContext.SaveChangesAsync();
                            }

                            return true;                                     
                        }
                        else
                        {
                            throw new Exception("Cadidate Not found");
                        }
                    }
                    else
                    {
                        throw new Exception("Counstituency & Candidate ID required");
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