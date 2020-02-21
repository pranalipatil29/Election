// ******************************************************************************
//  <copyright file="ApplicationSetting.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:   ApplicationSetting.cs
//  
//     Purpose: Get and Set JWT secret key and client url
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
    using System.Text;

    /// <summary>
    /// this class is used to get or set secret key and values
    /// </summary>
    public class ApplicationSetting
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        /// <value>
        /// The JWT secret.
        /// </value>
        public string JWTSecret { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>
        /// The client URL.
        /// </value>
        public string ClientURl { get; set; }

        /// <summary>
        /// Gets or sets the name of the cloud.
        /// </summary>
        /// <value>
        /// The name of the cloud.
        /// </value>
        public string CloudName { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string APIkey { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>
        /// The API secret.
        /// </value>
        public string APISecret { get; set; }

    }
}
