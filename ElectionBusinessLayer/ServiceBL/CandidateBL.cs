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
    using System.Threading.Tasks;
    using ElectionBusinessLayer.InterfaceBL;
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
    }
}
