// ******************************************************************************
//  <copyright file="CandidateBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateBL.cs
//  
//     Purpose:  Implementing business logic for apllication
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionBusinessLayer.ServiceBL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Candidate;
    using ElectionRepositoryLayer.InterfaceRL;

    /// <summary>
    /// this class is used to check the business logic of application
    /// </summary>
    /// <seealso cref="ElectionBusinessLayer.InterfaceBL.ICandidateBL" />
    public class CandidateBL : ICandidateBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly ICandidateRL candidateRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartiesBL"/> class.
        /// </summary>
        /// <param name="partieRL">The partie rl.</param>
        public CandidateBL(ICandidateRL candidateRL)
        {
            this.candidateRL = candidateRL;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="cadidateRequest"></param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AddCandidate(string emailID, CandidateRequest cadidateRequest)
        {
            try
            {
                // check whether admin entered any null value or not
                if (cadidateRequest != null)
                {
                    // return the operation result
                    return await this.candidateRL.AddCandidate(emailID, cadidateRequest);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the candidate records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception"> return exception</exception>
        public IList<CandidateResponse> DisplayCandidateRecords(string emailID)
        {
            try
            {
                return this.candidateRL.DisplayCandidateRecords(emailID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteCandidate(int candidateID, string emailID)
        {
            try
            {
                if (candidateID > 0)
                {
                    return await this.candidateRL.DeleteCandidate(candidateID, emailID);
                }
                else
                {
                    throw new Exception("Candidate ID is Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Candidate ID's are required
        /// or
        /// </exception>
        public async Task<bool> DeleteBulk(BulkCandidateRequest bulkRequest, string adminID)
        {
            try
            {
                // check wheather admin provide any null value or not
                if (bulkRequest != null)
                {
                    return await this.candidateRL.DeleteBulk(bulkRequest, adminID);
                }
                else
                {
                    throw new Exception("Candidate ID's are required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the information.
        /// </summary>
        /// <param name="candidateRequest">The candidate request.</param>
        /// <param name="partyID">The party identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<CandidateModel> UpdateInfo(CandidateRequest candidateRequest, int partyID, string adminID)
        {
            try
            {
                // check wheather admin entered any null value or not
                if (candidateRequest != null)
                {
                    return await this.candidateRL.UpdateInfo(candidateRequest, partyID, adminID);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
