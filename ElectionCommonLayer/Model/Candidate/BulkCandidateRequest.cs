// ******************************************************************************
//  <copyright file="BulkCandidateRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  BulkCandidateRequest.cs
//  
//     Purpose:  defining properties required to bulk candidates
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Candidate
{
    // Including the requried assemblies in to the program
    using System.Collections.Generic;

    /// <summary>
    /// defining properties required to bulk candidates
    /// </summary>
    public class BulkCandidateRequest
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
       public IList<int> CandidateID { get; set; }
    }
}
