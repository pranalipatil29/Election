// ******************************************************************************
//  <copyright file="PartiesRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  PartiesRL.cs
//  
//     Purpose: Implementing Add, Display, Update & Delete functionality for party
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Party;
    using ElectionRepositoryLayer.Context;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///  this class contains different methods to interact with Party table
    /// </summary>
    /// <seealso cref="ElectionRepositoryLayer.InterfaceRL.IPartieRL" />
    public class PartiesRL : IPartieRL
    {
        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// The user manager reference
        /// </summary>
        private readonly UserManager<ApplicationModel> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartiesRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        public PartiesRL(AuthenticationContext authenticationContext, UserManager<ApplicationModel> userManager)
        {
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Adds the party.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="partyRequest">The party request.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> AddParty(string emailID, PartyRequest partyRequest)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the required data
                    var data = new PartyModel()
                    {
                        PartyName = partyRequest.PartyName,
                        RegisterBy = partyRequest.RegisterBy,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    // add data in party table
                    this.authenticationContext.Parties.Add(data);

                    // save the change into database
                    await this.authenticationContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new Exception("Not Authorized Account");
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
        /// <exception cref="Exception">return exception</exception>
        public IList<PartyResponse> DisplayPartyRecords(string emailID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the record from Parties table
                    var data = this.authenticationContext.Parties.Select(s => s);

                    // create variable to store list of Partie info
                    var list = new List<PartyResponse>();

                    // check wheather the data contains any null value or not
                    if (data != null)
                    {
                        // iterates the loop for each record
                        foreach (var record in data)
                        {
                            var party = new PartyResponse()
                            {
                                PartyID = record.PartyID,
                                PartyName = record.PartyName,
                                RegisterBy = record.RegisterBy
                            };

                            // add the record in list
                            list.Add(party);
                        }

                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Not Authorized Account");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DeleteParty(int partyID, string emailID)
        {
            try
            {
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                if (adminData != null)
                {
                    var record = this.authenticationContext.Parties.Where(s => s.PartyID == partyID).FirstOrDefault();

                    if (record != null)
                    {
                        this.authenticationContext.Parties.Remove(record);
                        await this.authenticationContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("UnAuthorized Account");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
