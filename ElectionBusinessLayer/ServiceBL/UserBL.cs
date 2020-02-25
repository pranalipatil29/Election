using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using ElectionCommonLayer.Model.Vote;
using ElectionRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.ServiceBL
{
   public class UserBL : IUserBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBL"/> class.
        /// </summary>
        /// <param name="adminRL">The user repository layer.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> Register(UserRegitration registrationModel)
        {
            try
            {
                // check wheather admin entered any null value or not
                if (registrationModel != null)
                {
                    return await this.userRL.Register(registrationModel);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Logins the specified log in model.
        /// </summary>
        /// <param name="logInModel">The log in model.</param>
        /// <returns>
        /// returns admin details or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<UserResponse> Login(UserLogin logInModel)
        {
            try
            {
                // check wheather admin entered any null value or not
                if (logInModel != null)
                {
                    return await this.userRL.Login(logInModel);
                }
                else
                {
                    throw new Exception("Data Required");
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
        /// <exception cref="Exception">
        /// Invalid token
        /// or
        /// </exception>
        public async Task<string> GenerateToken(UserResponse accountResponse)
        {
            try
            {
                // check wheather token is generated or not
                if (accountResponse != null)
                {
                    var result = await this.userRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns>returns admin detail or null value</returns>
        /// <exception cref="Exception">
        /// Please select correct image
        /// or
        /// </exception>
        public async Task<UserResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // ckeck whether admin passed correct image info or not
                if (formFile != null)
                {
                    // pass user email id and image to repository layer method
                    return await this.userRL.ChangeProfilePicture(emailID, formFile);
                }
                else
                {
                    throw new Exception("Please select correct image");
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
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> GiveVote(string emailID, VoteRequest voteRequest)
        {
            try
            {
                if (voteRequest != null)
                {
                    return await this.userRL.GiveVote(emailID, voteRequest);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }      
    }
}
