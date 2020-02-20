// ******************************************************************************
//  <copyright file="CandidateRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateRequest.cs
//  
//     Purpose:  defining properties required to get candidate information
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
using System.ComponentModel.DataAnnotations;

namespace ElectionCommonLayer.Model.Candidate
{
    // Including the requried assemblies in to the program

    /// <summary>
    /// defining properties required to get candidate information
    /// </summary>
    public class CandidateRequest
    {
        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        public string CandidateName { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the voter identifier.
        /// </summary>
        /// <value>
        /// The voter identifier.
        /// </value>
        [Required]
        public string VoterID { get; set; }
    }
}
