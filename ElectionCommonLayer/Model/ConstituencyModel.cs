// ******************************************************************************
//  <copyright file="ConstituencyModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyModel.cs
//  
//     Purpose:  Creating columns for Constituency table
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
    /// this class is used to define Constituency table columns
    /// </summary>
    public class ConstituencyModel
    {
        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        [Key]
        public int ConstituencyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string State { get; set; }

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
