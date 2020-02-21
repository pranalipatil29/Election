// ******************************************************************************
//  <copyright file="ConstituencyRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyRequest.cs
//  
//     Purpose:  defining properties required to get Constituency Request
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Constituency
{
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///  defining properties required to get constituency info
    /// </summary>
    public class ConstituencyRequest
    {
        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
    }
}
