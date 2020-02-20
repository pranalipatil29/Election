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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model.Candidate;
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
        /// Initializes a new instance of the <see cref="PartyController"/> class.
        /// </summary>
        /// <param name="partiesBL">The parties business layer.</param>
        public CandidateController(ICandidateBL candidateBL)
        {
            this.candidateBL = candidateBL;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="candidateRequest">The candidate request.</param>
        /// <returns>returns the result indicating operation result</returns>
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
    }
}