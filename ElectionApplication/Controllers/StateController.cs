using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        /// <summary>
        /// The State business layer
        /// </summary>
        private readonly IStateBL stateBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstituencyController"/> class.
        /// </summary>
        /// <param name="stateBL">The State business layer.</param>
        public StateController(IStateBL stateBL)
        {
            this.stateBL = stateBL;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(StateRegistration stateRegistration)
        {
            try
            {
                // get the admin Email ID
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.stateBL.Registration(emailID, stateRegistration);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "State Record Added" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Register State" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Gets the states information.
        /// </summary>
        /// <returns>returns the operation result</returns>
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                // get the admin Email ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var records = this.stateBL.DisplayStates(adminID);

                // check wheather records variable contains any record or not
                if (records.Count > 0)
                {
                    return this.Ok(new { success = true, records });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Records not found" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

       
        [HttpDelete]
        public async Task<IActionResult> DeleteState(int stateID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.stateBL.DeleteStates(adminID, stateID);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Record Deleted" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to delete record" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
                    
        [HttpPut]
        public async Task<IActionResult> Update(StateRequest stateRequest)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var data = await this.stateBL.Update(adminID, stateRequest);

                // check wheather result contains any null value or not
                if (data != null)
                {
                    return this.Ok(new { success = true, message = "Successfully Updated State Info", data });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Update Info" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
    }
}