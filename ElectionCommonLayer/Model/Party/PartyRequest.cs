// ******************************************************************************
//  <copyright file="PartyRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  PartyRequest.cs
//  
//     Purpose:  defining properties required to get party information
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Party
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// this class contain properties of party model
    /// </summary>
    public class PartyRequest
    {
        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
       public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the register by.
        /// </summary>
        /// <value>
        /// The register by.
        /// </value>
       public string RegisterBy { get; set; }
    }
}
