// ******************************************************************************
//  <copyright file="ConstituencyResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyResponse.cs
//  
//     Purpose:  defining properties required to give Constituency Response
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
    /// defining properties required to give constituency info
    /// </summary>
    public class ConstituencyResponse
    {
        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        public int ConstituencyID { get; set; }

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
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int StateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string StateName { get; set; }
    }
}
