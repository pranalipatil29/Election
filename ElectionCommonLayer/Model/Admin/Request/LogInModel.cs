﻿// ******************************************************************************
//  <copyright file="LogInModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LogInModel.cs
//  
//     Purpose:  Defining properties for login functionality
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Admin.Request
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// defines properties for login functionality
    /// </summary>
    public class LogInModel
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
