﻿// ******************************************************************************
//  <copyright file="IPartiesRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IPartiesRL.cs
//  
//     Purpose:  Creating interface for repository layer
//     @author  Pranali Patil
//     @version 1.0
//     @since  20-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.InterfaceRL
{
    using ElectionCommonLayer.Model;
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model.Party;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// creating interface for repositosry layer
    /// </summary>
    public interface IPartiesRL
    {
        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="partyModel">The party Model.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddParty(string emailID, PartyModel partyModel);

        /// <summary>
        /// Displays the party records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        IList<PartyResponse> DisplayPartyRecords(string emailID);

        /// <summary>
        /// Deletes the party.
        /// </summary>
        /// <param name="partyID">The party identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns> return true or false indicating operation result</returns>
        Task<bool> DeleteParty(int partyID, string emailID);

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns> return true or false indicating operation result </returns>
        Task<bool> DeleteBulk(BulkRequest bulkRequest, string adminID);


        /// <summary>
        /// Updates the information.
        /// </summary>
        /// <param name="partyRequest">The party request.</param>
        /// <param name="partyID">The party identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>returns party info or null value</returns>
        Task<PartyModel> UpdateInfo(PartyRequest partyRequest, int partyID, string adminID);
    }
}
