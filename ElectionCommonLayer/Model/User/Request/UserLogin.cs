// ******************************************************************************
//  <copyright file="UserLogin.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  UserLogin.cs
//  
//     Purpose:  Defining properties for login functionality for user
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.User.Request
{
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// defines properties for login functionality
    /// </summary>
    public class UserLogin
    {

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        [Required]
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
