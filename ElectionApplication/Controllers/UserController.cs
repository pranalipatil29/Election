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
                var result = await this.userBL.GiveVote(voteRequest);

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