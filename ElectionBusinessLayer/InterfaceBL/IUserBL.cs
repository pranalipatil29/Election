using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.InterfaceBL
{
   public interface IUserBL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>returns true or false indicating operation result</returns>
        Task<bool> Register(UserRegitration registrationModel);

        /// <summary>
        /// Logins the specified log in model.
        /// </summary>
        /// <param name="logInModel">The log in model.</param>
        /// <returns> returns user details or null value</returns>
        Task<UserResponse> Login(UserLogin logInModel);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns>returns token or null value</returns>
        Task<string> GenerateToken(UserResponse accountResponse);

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns> returns user details or null value</returns>
        Task<UserResponse> ChangeProfilePicture(string emailID, IFormFile file);

        /// <summary>
        /// Gives the vote.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="voteRequest">The vote request.</param>
        /// <returns>returns true or false indicating operation result</returns>
        Task<bool> GiveVote(string emailID, VoteRequest voteRequest);
    }
}
