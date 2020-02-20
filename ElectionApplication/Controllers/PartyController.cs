// ******************************************************************************
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
    using ElectionCommonLayer.Model.Party;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// his class contains different methods to handle API calls for parties
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
        /// <param name="partyRequest">The party request.</param>
        /// <returns> returns the result indicating operation result</returns>
        [HttpPost]
        public async Task<IActionResult> AddParty(PartyRequest partyRequest)
        {
            try
            {
                // get the admin Email ID
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.partiesBL.AddParty(emailID, partyRequest);

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
        /// <returns></returns>
        [HttpDelete]
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
                    return this.Ok(new { success = true, message = "Parties Delete" });
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
    }
}