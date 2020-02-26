// ******************************************************************************
//  <copyright file="ConstituencyBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyBL.cs
//  
//     Purpose:  Implementing business logic for apllication
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionBusinessLayer.ServiceBL
{
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Constituency;
    using ElectionRepositoryLayer.InterfaceRL;
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class ConstituencyBL : IConstituencyBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IConstituencyRL constituencyRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstituencyBL"/> class.
        /// </summary>
        /// <param name="constituencyRL">The constituency rl.</param>
        public ConstituencyBL(IConstituencyRL constituencyRL)
        {
            this.constituencyRL = constituencyRL;
        }

        public async Task<bool> AddConstituency(string emailID, RegisterConstituency registerConstituency)
        {
            try
            {
                // check whether admin entered any null value or not
                if (registerConstituency != null)
                {
                    // return the operation result
                    return await this.constituencyRL.AddConstituency(emailID, registerConstituency);
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
        /// Displays the constituency records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns constituency records or null value
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public IList<ConstituencyResponse> DisplayConstituencyRecords(string emailID)
        {
            try
            {
                return this.constituencyRL.DisplayConstituencyRecords(emailID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the constituency.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Constituency ID is Required
        /// or
        /// </exception>
        public async Task<bool> DeleteConstituency(int constituencyID, string emailID)
        {
            try
            {
                if (constituencyID > 0)
                {
                    return await this.constituencyRL.DeleteConstituency(constituencyID, emailID);
                }
                else
                {
                    throw new Exception("Constituency ID is Required");
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
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Contituency ID's are required
        /// or
        /// </exception>
        public async Task<bool> DeleteBulk(BulkConstituencyRequest bulkRequest, string adminID)
        {
            try
            {
                // check wheather admin provide any null value or not
                if (bulkRequest != null)
                {
                    return await this.constituencyRL.DeleteBulk(bulkRequest, adminID);
                }
                else
                {
                    throw new Exception("Contituency ID's are required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public async Task<ConstituencyModel> UpdateInfo(ConstituencyRequest constituencyRequest, int constituencyID, string adminID)
        {
            try
            {
              //  check wheather admin entered correct Constituency ID
                if (constituencyID > 0)
                {
                    return await this.constituencyRL.UpdateInfo(constituencyRequest, constituencyID, adminID);
                }
                else
                {
                    throw new Exception("Constituency ID Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IList<ConstituenciesListResponse> GetConstituenciesList(int stateID)
        {
            try
            {
                if (stateID > 0)
                {
                    return this.constituencyRL.GetConstituenciesList(stateID);
                }
                else
                {
                    throw new Exception("StateID is invalid ");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
