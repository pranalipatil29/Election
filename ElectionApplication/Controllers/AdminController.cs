﻿// ******************************************************************************
//  <copyright file="AdminController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminController.cs
//  
//     Purpose:  Creating a controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-2-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionApplication.Controllers
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ElectionBusinessLayer.InterfaceBL;
    using ElectionCommonLayer.Model.Admin.Request;
    using ElectionCommonLayer.Model.Party;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different methods to handle API calls for admin
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// creating reference of Admin business layer class
        /// </summary>
        private readonly IAdminBL adminBL;

        /// <summary>
        /// Initializes a new instance of the AdminController class
        /// </summary>
        /// <param name="adminBL"> instance of busines layer Admin class</param>
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            try
            {
                // register new account info
                var result = await this.adminBL.Register(registrationModel);

                // check wheather the result indicate true or false
                if (result)
                {
                    return this.Ok(new { success = true, message = "Registration successfully completed" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Failed" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        /// <summary>
        /// API for Log in functionality
        /// </summary>
        /// <param name="loginModel"> login model indicates values for login fuinctionality</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LogInModel loginModel)
        {
            // checking login information
            var data = await this.adminBL.Login(loginModel);

            // check whether user get login or not
            if (data != null)
            {
                // generate the token
                var token = await this.adminBL.GenerateToken(data);
                return this.Ok(new { success = true, message = "Login Successfull", token, data });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Login Failed" });
            }
        }

        /// <summary>
        /// API for Uploading Profile Picture
        /// </summary>
        /// <param name="formFile"> holds the image information</param>
        /// <returns>returns message indicating operation is successful or not</returns>
        [HttpPut]
        [Route("ProfilePicture")]
        public async Task<IActionResult> ProfilePictureUpload(IFormFile formFile)
        {
            try
            {
                // get the email ID of admin
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // pass the email ID and image to business layer method of admin
                var data = await this.adminBL.ChangeProfilePicture(emailID, formFile);

                // check whether data is null or not
                if (data != null)
                {                   
                    return this.Ok(new { success = true, message = "Profile Picture Uploaded", data });
                }
                else
                {
                    return this.Ok(new { success = false, message = "Failed to upload Profile Picture" });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Party")]
        public async Task<IActionResult> AddParty(PartyRequest partyRequest)
        {
            try
            {
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                var result = await this.adminBL.AddParty(partyRequest);

                if (result)
                {
                    return this.Ok(new { success = true, message = "Party Record Added" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to Register Party" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
    }
}