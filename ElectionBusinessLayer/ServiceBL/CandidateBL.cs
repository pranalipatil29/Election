﻿// ******************************************************************************
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
    // Including the requried assemblies in to the program
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
        /// Initializes a new instance of the <see cref="CandidateBL"/> class.
        /// </summary>
        /// <param name="candidateRL">The candidate repository layer.</param>
        public CandidateBL(ICandidateRL candidateRL)
        {
            this.candidateRL = candidateRL;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="candidateModel"> the candidate Model.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> AddCandidate(string emailID, CandidateRequest candidateModel)
        {
            try
            {
                // check whether admin entered any null value or not
                if (candidateModel != null)
                {
                    // return the operation result
                    return await this.candidateRL.AddCandidate(emailID, candidateModel);
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

        /// <summary>
        /// Deletes the candidate record.
        /// </summary>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Candidate ID is Required
        /// or
        /// </exception>
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
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Candidate ID Required
        /// or
        /// </exception>
        public async Task<CandidateModel> UpdateInfo(UpdateRequest updateRequest, int candidateID, string adminID)
        {
            try
            {
                // check wheather admin entered correct Candidate ID
                if (candidateID > 0)
                {
                    return await this.candidateRL.UpdateInfo(updateRequest, candidateID, adminID);
                }
                else
                {
                    throw new Exception("Candidate ID Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the constituencywise candidates.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// returns the candidates list or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Please provide correct Contituency ID & State Name
        /// or
        /// </exception>
        public IList<ConstituencywiseCandidates> GetConstituencywiseCandidates(int constituencyID, int stateID)
        {
            try
            {
                if (constituencyID > 0 && stateID > 0)
                {
                    return this.candidateRL.GetConstituencywiseCandidates(constituencyID, stateID);
                }
                else
                {
                    throw new Exception("Please provide correct Contituency ID & State ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
