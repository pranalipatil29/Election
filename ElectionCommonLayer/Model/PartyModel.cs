// ******************************************************************************
//  <copyright file="PartyModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  PArtyModel.cs
//  
//     Purpose:  Creating columns for Party table
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
    /// this class is used to define Party table columns
    /// </summary>
    public class PartyModel
    {
        /// <summary>
        /// Gets or sets the party identifier.
        /// </summary>
        /// <value>
        /// The party identifier.
        /// </value>
        [Key]
        public int PartyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the register by.
        /// </summary>
        /// <value>
        /// The register by.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string RegisterBy { get; set; }

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
    }
}
