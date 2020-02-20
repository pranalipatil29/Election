// ******************************************************************************
//  <copyright file="AccountResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AccountResponse.cs
//  
//     Purpose:  Defining properties for handling account response
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// *****************************************************************************
namespace ElectionCommonLayer.Model.Admin.Respone
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// defines properties for account respone
    /// </summary>
    public class AccountResponse
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        public string ProfilePicture { get; set; }
    }
}
