// ******************************************************************************
//  <copyright file="ICandidateRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ICandidateRL.cs
//  
//     Purpose:  Creating interface for repository layer
//     @author  Pranali Patil
//     @version 1.0
//     @since  21-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.InterfaceRL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Constituency;

    /// <summary>
    /// creating interface for repository layer
    /// </summary>
    public interface IConstituencyRL
    {
        /// <summary>
        /// Adds the Constituency.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="constituencyModel">The constituency Model.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> AddConstituency(string emailID, ConstituencyModel constituencyModel);

        /// <summary>
        /// Displays the constituency records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns> returns constituency records or null value</returns>
        IList<ConstituencyResponse> DisplayConstituencyRecords(string emailID);

        /// <summary>
        /// Deletes the constituency.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> DeleteConstituency(int constituencyID, string emailID);

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> DeleteBulk(BulkConstituencyRequest bulkRequest, string adminID);

        /// <summary>
        /// Updates the information.
        /// </summary>
        /// <param name="constituencyRequest">The constituency request.</param>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>returns constituency records or null value</returns>
        Task<ConstituencyModel> UpdateInfo(ConstituencyRequest constituencyRequest, int constituencyID, string adminID);

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns>return the states or null value</returns>
        IList<string> GetStates();

       IList<ConstituenciesListResponse> GetConstituenciesList(string state);
    }
}
