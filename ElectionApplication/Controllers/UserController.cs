using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.Vote;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// creating reference of User business layer class
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// Initializes a new instance of the UserController class
        /// </summary>
        /// <param name="userBL"> instance of business layer user class</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register(UserRegitration registrationModel)
        {
            try
            {
                // register new account info
                var result = await this.userBL.Register(registrationModel);

                // check wheather the result indicate true or false
                if (result)
                {
                    return this.Ok(new { success = true, message = "Registration successfully completed" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// API for Log in functionality
        /// </summary>
        /// <param name="loginModel"> login model indicates values for login functionality</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLogin loginModel)
        {
            // checking login information
            var data = await this.userBL.Login(loginModel);

            // check whether user get login or not
            if (data != null)
            {
                // generate the token
                var token = await this.userBL.GenerateToken(data);
                return this.Ok(new { success = true, message = "Login Successfull", token, data });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Login Failed" });
            }
        }

        /// <summary>
        /// API for Uploading Profile Picture
        /// </summary>
        /// <param name="formFile"> holds the image information</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPut]
        [Route("ProfilePicture")]
        public async Task<IActionResult> ProfilePictureUpload(IFormFile formFile)
        {
            try
            {
                // get the email ID of admin
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // pass the email ID and image to business layer method of admin
                var data = await this.userBL.ChangeProfilePicture(emailID, formFile);

                // check whether data is null or not
                if (data != null)
                {
                    return this.Ok(new { success = true, message = "Profile Picture Uploaded", data });
                }
                else
                {
                    return this.Ok(new { success = false, message = "Failed to upload Profile Picture" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// API for saving Votes
        /// </summary>
        /// <param name="voteRequest"></param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPut]
        [Route("Vote")]
        public async Task<IActionResult> GiveVote(VoteRequest voteRequest)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                var result = await this.userBL.GiveVote(userID, voteRequest);

                if (result)
                {
                    return this.Ok(new { success = true, message = "Your Vote is Submitted" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To submit Vote" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { succes = false, message = exception.Message });
            }
        }       
    }
}