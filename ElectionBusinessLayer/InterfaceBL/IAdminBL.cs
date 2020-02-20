// ******************************************************************************
//  <copyright file="IAdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAdminBL.cs
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
    using ElectionCommonLayer.Model.Admin;
    using ElectionCommonLayer.Model.Admin.Request;
    using ElectionCommonLayer.Model.Admin.Respone;
    using ElectionCommonLayer.Model.Party;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///  creating interface for business layer
    /// </summary>
    public interface IAdminBL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns true or false indicating operation result</returns>
        Task<bool> Register(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified log in model.
        /// </summary>
        /// <param name="logInModel">The log in model.</param>
        /// <returns> returns admin details or null value</returns>
        Task<AccountResponse> Login(LogInModel logInModel);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns> returns token or null value</returns>
        Task<string> GenerateToken(AccountResponse accountResponse);

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns> returns admin details or null value</returns>
        Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile file);

        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="partyRequest">The party request.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> AddParty(PartyRequest partyRequest);

        /// <summary>
        /// Displays the party records.
        /// </summary>
        /// <returns>returns party records or null value</returns>
        IList<PartyResponse> DisplayPartyRecords();
    }
}
