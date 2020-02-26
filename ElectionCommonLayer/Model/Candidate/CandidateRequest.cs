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
namespace ElectionCommonLayer.Model.Candidate
{
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// defining properties required to get candidate information
    /// </summary>
    public class CandidateRequest
    {
        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        public int ConstituencyID { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int StateID { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        public int PartyID { get; set; }     
                
    }
}
