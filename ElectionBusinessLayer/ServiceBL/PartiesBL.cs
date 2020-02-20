// ******************************************************************************
//  <copyright file="PartiesBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  PartiesBL.cs
//  
//     Purpose:  Implementing business logic for apllication
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionBusinessLayer.ServiceBL
{
    // Including the requried assemblies in to the program
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model.Party;
    using ElectionRepositoryLayer.InterfaceRL;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///  this class is used to check the business logic of application
    /// </summary>
    /// <seealso cref="ElectionBusinessLayer.InterfaceBL.IPartiesBL" />
   public class PartiesBL : IPartiesBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IPartieRL partiesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartiesBL"/> class.
        /// </summary>
        /// <param name="partieRL">The partie rl.</param>
        public PartiesBL(IPartieRL partieRL)
        {
            this.partiesRL = partieRL;
        }

        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="partyRequest">The party request.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> AddParty(string emailID, PartyRequest partyRequest)
        {
            try
            {
                // check whether admin entered any null value or not
                if (partyRequest != null)
                {
                    // return the operation result
                    return await this.partiesRL.AddParty(emailID, partyRequest);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the party records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception"> return exception</exception>
        public IList<PartyResponse> DisplayPartyRecords(string emailID)
        {
            try
            {
                return this.partiesRL.DisplayPartyRecords(emailID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the party.
        /// </summary>
        /// <param name="partyID">The party identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Party ID is Required
        /// or
        /// </exception>
        public async Task<bool> DeleteParty(int partyID, string emailID)
        {
            try
            {
                if (partyID > 0)
                {
                    return await this.partiesRL.DeleteParty(partyID, emailID);
                }
                else
                {
                    throw new Exception("Party ID is Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Party ID's are required
        /// or
        /// </exception>
        public async Task<bool> DeleteBulk(BulkRequest bulkRequest, string adminID)
        {
            try
            {
                // check wheather admin provide any null value or not
                if (bulkRequest != null)
                {
                    return await this.partiesRL.DeleteBulk(bulkRequest, adminID);
                }
                else
                {
                    throw new Exception("Party ID's are required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
