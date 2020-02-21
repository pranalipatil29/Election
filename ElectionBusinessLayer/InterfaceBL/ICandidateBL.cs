// ******************************************************************************
//  <copyright file="ICandidateBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ICandidateBL.cs
//  
//     Purpose:  Creating interface for business layer
//     @author  Pranali Patil
//     @version 1.0
//     @since  20-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionBusinessLayer.InterfaceBL
{
    // Including the requried assemblies in to the program
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Candidate;

    /// <summary>
    /// creating interface for business layer
    /// </summary>
    public interface ICandidateBL
    {
        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="candidateModel">The candidate Model.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddCandidate(string emailID, CandidateModel candidateModel);

        /// <summary>
        /// Displays the candidate records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        IList<CandidateResponse> DisplayCandidateRecords(string emailID);
        
        /// <summary>
        /// Deletes the candidate record.
        /// </summary>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>returns true or false indicating operation result</returns>
        Task<bool> DeleteCandidate(int candidateID, string emailID);

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns> return true or false indicating operation result </returns>
        Task<bool> DeleteBulk(BulkCandidateRequest bulkRequest, string adminID);

        /// <summary>
        /// Updates the information.
        /// </summary>
        /// <param name="candidateRequest">The candidate request.</param>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>return true or false indicating operation result</returns>
        Task<CandidateModel> UpdateInfo(CandidateRequest candidateRequest, int candidateID, string adminID);
    }
}
