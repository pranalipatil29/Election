// ******************************************************************************
//  <copyright file="IPartiesBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IPartiesBL.cs
//  
//     Purpose:  Creating interface for business layer
//     @author  Pranali Patil
//     @version 1.0
//     @since  20-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionBusinessLayer.InterfaceBL
{
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model.Party;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ElectionCommonLayer.Model;

    /// <summary>
    ///  creating interface for business layer
    /// </summary>
    public interface IPartiesBL
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
        /// <returns> returns updated info or null value</returns>
        Task<PartyModel> UpdateInfo(PartyRequest partyRequest, int partyID, string adminID);
    }
}
