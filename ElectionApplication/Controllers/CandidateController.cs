// ******************************************************************************
//  <copyright file="CandidateController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateController.cs
//  
//     Purpose:  Creating a Candidate controller to manage api calls
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
    using ElectionCommonLayer.Model.Candidate;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///  this class contains different methods to handle API calls for Candidates
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        /// <summary>
        /// creating reference of Party business layer class
        /// </summary>
        private readonly ICandidateBL candidateBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateController"/> class.
        /// </summary>
        /// <param name="candidateBL">The candidate business layer.</param>
        public CandidateController(ICandidateBL candidateBL)
        {
            this.candidateBL = candidateBL;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="candidateRequest">The candidate request.</param>
        /// <returns>returns the result indicating operation result</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateRequest candidateRequest)
        {
            try
            {
                // get the admin Email ID
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.candidateBL.AddCandidate(emailID, candidateRequest);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Candidate Record Added" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Register Candidate" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Gets the candidates information.
        /// </summary>
        /// <returns>returns the result indicating operation result</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCandidatesInfo()
        {
            try
            {
                // get the admin Email ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var records = this.candidateBL.DisplayCandidateRecords(adminID);

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
        /// Deletes the candidate.
        /// </summary>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <returns> returns the result indicating operation result</returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCandidate(int candidateID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.candidateBL.DeleteCandidate(candidateID, adminID);

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
        /// <returns> returns the result indicating operation result</returns>
        [Authorize]
        [HttpDelete]
        [Route("BulkCandidates")]
        public async Task<IActionResult> BulkParties(BulkCandidateRequest bulkRequest)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var result = await this.candidateBL.DeleteBulk(bulkRequest, adminID);

                // check wheather result indicates true value or not
                if (result)
                {
                    return this.Ok(new { success = true, message = "Candidates record Delete" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Candidate Record Not Found" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// Updates the specified candidate request.
        /// </summary>
        /// <param name="candidateRequest">The candidate request.</param>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <returns>returns the result indicating operation result</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRequest updateRequest, int candidateID)
        {
            try
            {
                // get the admin ID
                var adminID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // get the operation result
                var data = await this.candidateBL.UpdateInfo(updateRequest, candidateID, adminID);

                // check wheather result contains any null value or not
                if (data != null)
                {
                    return this.Ok(new { success = true, message = "Successfully Updated Candidate Info", data });
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
        /// Gets the candidates in constituency.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="stateID">The state identifier.</param>
        /// <returns>returns the result indicating operation result</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("CandidatesInConstituency/{constituencyID}/{stateID}")]
        public async Task<IActionResult> GetCandidatesInConstituency(int constituencyID, int stateID)
        {
            try
            {
                var data = this.candidateBL.GetConstituencywiseCandidates(constituencyID, stateID);

                if (data.Count > 0)
                {
                    return this.Ok(new { success = true, data });
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
    }
}