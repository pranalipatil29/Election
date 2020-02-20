// ******************************************************************************
//  <copyright file="ICadidateBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ICadidateBL.cs
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
    using System.Threading.Tasks;
    using ElectionCommonLayer.Model.Candidate;

    /// <summary>
    /// creating interface for business layer
    /// </summary>
    public interface ICandidateBL
    {
        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="partyRequest">The party request.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddCandidate(string emailID, CandidateRequest cadidateRequest);
    }
}
