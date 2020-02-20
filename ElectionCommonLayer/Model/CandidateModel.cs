// ******************************************************************************
//  <copyright file="CandidateModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateModel.cs
//  
//     Purpose:  Creating columns for Candidate table
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// this class is used to define candidate table column
    /// </summary>
    public class CandidateModel
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        [Key]
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string CandidateName { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        [ForeignKey("ConstituencyModel")]
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        [ForeignKey("PartyName")]
        public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the voter identifier.
        /// </summary>
        /// <value>
        /// The voter identifier.
        /// </value>
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string VoterID { get; set; }
    }
}
