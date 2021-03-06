﻿// ******************************************************************************
//  <copyright file="PartyController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  PartyController.cs
//  
//     Purpose:  Creating a Party controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-2-2020
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
    using ElectionCommonLayer.Model.Party;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different methods to handle API calls for parties
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        /// <summary>
        /// creating reference of Party business layer class
        /// </summary>
        private readonly IPartiesBL partiesBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartyController"/> class.
        /// </summary>
        /// <param name="partiesBL">The parties business layer.</param>
        public PartyController(IPartiesBL partiesBL)
        {
            this.partiesBL = partiesBL;
        }

        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="partyModel">The party Model.</param>
        /// <returns> returns the result indicating operation result</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddParty(PartyModel partyModel)
        {
            try
            {
                // get the admin Email ID
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.partiesBL.AddParty(emailID, partyModel);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Party Record Added" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Register Party" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Gets the parties information.
        /// </summary>
        /// <returns>returns the result indicating operation result</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPartiesInfo()
        {
            try
            {
                // get the admin Email ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var records = this.partiesBL.DisplayPartyRecords(adminID);

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
        /// Deletes the party.
        /// </summary>
        /// <param name="partyID">The party identifier.</param>
        /// <returns>returns the result indicating operation result</returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteParty(int partyID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.partiesBL.DeleteParty(partyID, adminID);

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
        /// Deletes the parties.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <returns>returns the result indicating operation result</returns>
        [Authorize]
        [HttpPost]
        [Route("BulkParties")]
        public async Task<IActionResult> DeleteParties(BulkRequest bulkRequest)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.partiesBL.DeleteBulk(bulkRequest, adminID);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Parties record Delete" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Party Record Not Found" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Updates the specified party request.
        /// </summary>
        /// <param name="partyRequest">The party request.</param>
        /// <param name="partyID">The party identifier.</param>
        /// <returns>returns the result indicating operation result</returns>
        /// <exception cref="Exception"> return exception</exception>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(PartyRequest partyRequest, int partyID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var data = await this.partiesBL.UpdateInfo(partyRequest, partyID, adminID);

                // check wheather result contains any null value or not
                if (data != null)
                {
                    return this.Ok(new { success = true, message = "Successfully Updated Party Info", data });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Update Info" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new {success = false, message = exception.Message });
            }
        }
    }
}