// ******************************************************************************
//  <copyright file="IAdminRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAdminRL.cs
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
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model.Admin.Request;
    using ElectionCommonLayer.Model.Admin.Respone;
    using ElectionCommonLayer.Model.Party;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// creating interface for repository layer
    /// </summary>
    public interface IAdminRL
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
        /// Gets the result.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns> returns result or null value</returns>
        IList<ResultResponse> GetResult(string adminID);

        /// <summary>
        /// Costituencywises the ressult.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="state">The state.</param>
        /// <returns>returns Constituency wise result or null value</returns>
        IList<ResultResponse> CostituencywiseRessult(string adminID, int constituencyID, string state);

        ///// <summary>
        ///// Parties the wise result.
        ///// </summary>
        ///// <param name="adminID">The admin identifier.</param>
        ///// <param name="state">The state.</param>
        ///// <returns>returns Party wise Result or null value</returns>
        //IList<ResultResponse> PartyWiseResult(string adminID, string state);
    }
}
