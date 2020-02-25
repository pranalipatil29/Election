// ******************************************************************************
//  <copyright file="ICandidateRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ICandidateRL.cs
//  
//     Purpose:  Creating interface for Repository layer
//     @author  Pranali Patil
//     @version 1.0
//     @since  20-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.InterfaceRL
{
    using ElectionCommonLayer.Model;
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model.Candidate;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///  creating interface for repository layer
    /// </summary>
    public interface ICandidateRL
    {
        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="candidateRequest">The candidate Request.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddCandidate(string emailID, CandidateRequest candidateRequest);

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
        Task<CandidateModel> UpdateInfo(UpdateRequest updateRequest, int candidateID, string adminID);

        /// <summary>
        /// Gets the constituencywise candidates.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="state">The state.</param>
        /// <returns>returns the candidates list or null value</returns>
        IList<ConstituencywiseCandidates> GetConstituencywiseCandidates(int constituencyID, string state);
    }
}
