// ******************************************************************************
//  <copyright file="CandidateResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateResponse.cs
//  
//     Purpose:  defining properties required to provide Candidate information
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Candidate
{
    // Including the requried assemblies in to the program

    /// <summary>
    /// defining properties required to provide Candidate information
    /// </summary>
    public class CandidateResponse
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        public string CandidateName { get; set; }

        /// <summary>
        /// Gets or sets the voter identifier.
        /// </summary>
        /// <value>
        /// The voter identifier.
        /// </value>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        public int ConstituencyID { get; set; }

        /// <summary>
        /// Gets or sets the party identifier.
        /// </summary>
        /// <value>
        /// The party identifier.
        /// </value>
        public int PartyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        public string PartyName { get; set; }
    }
}
