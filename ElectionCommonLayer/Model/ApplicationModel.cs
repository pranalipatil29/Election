// ******************************************************************************
//  <copyright file="ApplicationModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ApplicationModel.cs
//  
//     Purpose:  Creating columns for admin table
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// this class is used to define Application model
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationModel : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string UserType { get; set; }

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
