// ******************************************************************************
//  <copyright file="ConstituencyController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyController.cs
//  
//     Purpose:  Creating a Candidate controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionApplication.Controllers
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Constituency;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ConstituencyController : ControllerBase
    {
        /// <summary>
        /// The constituency bl
        /// </summary>
        private readonly IConstituencyBL constituencyBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstituencyController"/> class.
        /// </summary>
        /// <param name="constituencyBL">The constituency bl.</param>
        public ConstituencyController(IConstituencyBL constituencyBL)
        {
            this.constituencyBL = constituencyBL;
        }

        /// <summary>
        /// Adds the constitute.
        /// </summary>
        /// <param name="constituencyRequest">The constituency request.</param>
        /// <returns>returns the operation result</returns>
        [HttpPost]
        public async Task<IActionResult> AddConstitute(ConstituencyModel constituencyModel)
        {
            try
            {
                // get the admin Email ID
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.constituencyBL.AddConstituency(emailID, constituencyModel);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Constituency Record Added" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Register Constitutency" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Gets the constituency information.
        /// </summary>
        /// <returns>returns the operation result</returns>
        [HttpGet]
        public async Task<IActionResult> GetConstituencyInfo()
        {
            try
            {
                // get the admin Email ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var records = this.constituencyBL.DisplayConstituencyRecords(adminID);

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

        /// <summary>
        /// Deletes the constituency.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <returns>returns the operation result</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteConstituency(int constituencyID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.constituencyBL.DeleteConstituency(constituencyID, adminID);

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

        /// <summary>
        /// Bulks the constituency.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <returns>returns the operation result</returns>
        [HttpDelete]
        [Route("BulkConstituencies")]
        public async Task<IActionResult> BulkConstituency(BulkConstituencyRequest bulkRequest)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.constituencyBL.DeleteBulk(bulkRequest, adminID);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Constituency record Delete" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Constituency Record Not Found" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Updates the specified constituency request.
        /// </summary>
        /// <param name="constituencyRequest">The constituency request.</param>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <returns>returns the operation result</returns>
        [HttpPut]
        public async Task<IActionResult> Update(ConstituencyRequest constituencyRequest, int constituencyID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var data = await this.constituencyBL.UpdateInfo(constituencyRequest, constituencyID, adminID);

                // check wheather result contains any null value or not
                if (data != null)
                {
                    return this.Ok(new { success = true, message = "Successfully Updated Constituency Info", data });
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

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns>returns the operation result</returns>
        [HttpGet]
        [Route("States")]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                var accountID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                var states = this.constituencyBL.GetStates();

                if (states.Count > 0)
                {
                    return this.Ok(new { success = true, states });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Load States" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        [HttpGet]
        [Route("Constituencies")]
        public async Task<IActionResult> GetConstituencies(string state)
        {
            try
            {
                var accountID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                var records = this.constituencyBL.GetConstituenciesList(state);

                if (records.Count > 0)
                {
                    return this.Ok(new { success = true, records });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Find Constituencies Records" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
    }
}