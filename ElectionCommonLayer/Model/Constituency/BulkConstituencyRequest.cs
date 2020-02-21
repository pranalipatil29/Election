// ******************************************************************************
//  <copyright file="BulkConstituencyRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  BulkConstituencyRequest.cs
//  
//     Purpose:  defining properties required to bulk Constituency
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionCommonLayer.Model.Constituency
{
    // Including the requried assemblies in to the program
    using System.Collections.Generic;

    /// <summary>
    /// defining properties required to bulk constituency
    /// </summary>
    public class BulkConstituencyRequest
    {
        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        public IList<int> ConstituencyID { get; set; }
    }
}
