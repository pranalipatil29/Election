// ******************************************************************************
//  <copyright file="AdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminBL.cs
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
    using ElectionCommonLayer.Model.Admin.Request;
    using ElectionCommonLayer.Model.Admin.Respone;
    using ElectionCommonLayer.Model.Party;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this class is used to check the business logic of application
    /// </summary>
    /// <seealso cref="ElectionBusinessLayer.InterfaceBL.IAdminBL" />
    public class AdminBL : IAdminBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IAdminRL adminRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBL"/> class.
        /// </summary>
        /// <param name="adminRL">The admin repository layer.</param>
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            try
            {
                // check wheather admin entered any null value or not
                if (registrationModel != null)
                {
                    return await this.adminRL.Register(registrationModel);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Logins the specified log in model.
        /// </summary>
        /// <param name="logInModel">The log in model.</param>
        /// <returns>
        /// returns admin details or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<AccountResponse> Login(LogInModel logInModel)
        {
            try
            {
                // check wheather admin entered any null value or not
                if (logInModel != null)
                {
                    return await this.adminRL.Login(logInModel);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns>
        /// returns token or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Invalid token
        /// or
        /// </exception>
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try
            {
                // check wheather token is generated or not
                if (accountResponse != null)
                {
                    var result = await this.adminRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns>returns admin detail or null value</returns>
        /// <exception cref="Exception">
        /// Please select correct image
        /// or
        /// </exception>
        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // ckeck whether admin passed correct image info or not
                if (formFile != null)
                {
                    // pass user email id and image to repository layer method
                    return await this.adminRL.ChangeProfilePicture(emailID, formFile);
                }
                else
                {
                    throw new Exception("Please select correct image");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

     
    }
}
