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
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model.Candidate;
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
        /// <param name="partyRequest">The party request.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddCandidate(string emailID, CandidateRequest cadidateRequest);
    }
}
