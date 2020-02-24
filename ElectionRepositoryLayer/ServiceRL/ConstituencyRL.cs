// ******************************************************************************
//  <copyright file="ConstituencyRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ConstituencyRL.cs
//  
//     Purpose: Implementing Add, Display, Update & Delete functionality for Constituency
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-02-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace ElectionRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Constituency;
    using ElectionRepositoryLayer.Context;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConstituencyRL : IConstituencyRL
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
        /// Initializes a new instance of the <see cref="ConstituencyRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        /// <param name="userManager">The user manager.</param>
        public ConstituencyRL(AuthenticationContext authenticationContext, UserManager<ApplicationModel> userManager)
        {
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Adds the Constituency.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="constituencyModel">The constituency Model.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Constituency Record Already Exist
        /// or
        /// Not Authorized Account
        /// or
        /// </exception>
        public async Task<bool> AddConstituency(string emailID, ConstituencyModel constituencyModel)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // find record for admin entered Constituency Name
                    var contituency = this.authenticationContext.Constituencies.Where(s => s.ConstituencyName == constituencyModel.ConstituencyName).FirstOrDefault();

                    // check wheather contituency record found from Constituency table or not
                    if (contituency == null)
                    {
                        // get the required data
                        var data = new ConstituencyModel()
                        {
                            City = constituencyModel.City,
                            State = constituencyModel.State,
                            ConstituencyName = constituencyModel.ConstituencyName,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };

                        // add data in Constituency table
                        this.authenticationContext.Constituencies.Add(data);

                        // save the change into database
                        await this.authenticationContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        throw new Exception("Constituency Record Already Exist");
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

        /// <summary>
        /// Displays the constituency records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns constituency records or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Not Authorized Account
        /// or
        /// </exception>
        public IList<ConstituencyResponse> DisplayConstituencyRecords(string emailID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the record from Constituency table
                    var data = this.authenticationContext.Constituencies.Select(s => s);

                    // create variable to store list of Constituency info
                    var list = new List<ConstituencyResponse>();

                    // check wheather the data contains any null value or not
                    if (data != null)
                    {
                        // iterates the loop for each record
                        foreach (var record in data)
                        {
                            var constituency = new ConstituencyResponse()
                            {
                                ConstituencyID = record.ConstituencyID,
                                ConstituencyName = record.ConstituencyName,
                                City = record.City,
                                State = record.State
                            };

                            // add the record in list
                            list.Add(constituency);
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

        /// <summary>
        /// Deletes the constituency.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Record Not Found
        /// or
        /// UnAuthorized Account
        /// or
        /// </exception>
        public async Task<bool> DeleteConstituency(int constituencyID, string emailID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the Constituency data
                    var record = this.authenticationContext.Constituencies.Where(s => s.ConstituencyID == constituencyID).FirstOrDefault();

                    // check wheather any record for Contituency found or not
                    if (record != null)
                    {
                        // get the candidates which have same constituency 
                        var candidates = this.authenticationContext.Candidates.Where(s => s.ConstituencyID == constituencyID);

                        if ( candidates != null)
                        {
                            foreach(var candidate in candidates)
                            {
                                this.authenticationContext.Candidates.Remove(candidate);
                            }
                        }
                        // remove the record from Constituency table
                        this.authenticationContext.Constituencies.Remove(record);

                        // save the changes into database
                        await this.authenticationContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        throw new Exception("Record Not Found");
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

        /// <summary>
        /// Deletes the bulk.
        /// </summary>
        /// <param name="bulkRequest">The bulk request.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> DeleteBulk(BulkConstituencyRequest bulkRequest, string adminID)
        {
            try
            {
                // flag variable is used to indicate operation result
                bool flag = false;

                // iterates the loop for each record
                foreach (var record in bulkRequest.ConstituencyID)
                {
                    // get the delete operation result
                    var result = await this.DeleteConstituency(record, adminID);

                    // check wheather operation is successfull or not
                    if (result)
                    {
                        flag = true;
                    }
                    else

                    {
                        flag = false;
                    }
                }

                // return the operation result
                return flag;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the information.
        /// </summary>
        /// <param name="constituencyRequest">The constituency request.</param>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// returns constituency records or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Constituency Record Not Found
        /// or
        /// UnAuthorized Account
        /// or
        /// </exception>
        public async Task<ConstituencyModel> UpdateInfo(ConstituencyRequest constituencyRequest, int constituencyID, string adminID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the constituency data
                    var constituencyInfo = this.authenticationContext.Constituencies.Where(s => s.ConstituencyID == constituencyID).FirstOrDefault();

                    // check wheather any record for constituency found or not
                    if (constituencyInfo != null)
                    {
                        // modify the date value
                        constituencyInfo.ModifiedDate = DateTime.Now;

                        // check wheather constituency name  property is null or empty value
                        if (constituencyRequest.ConstituencyName != null && constituencyRequest.ConstituencyName != string.Empty)
                        {
                            constituencyInfo.ConstituencyName = constituencyRequest.ConstituencyName;
                        }

                        // check wheather constituency City property is null or empty value
                        if (constituencyRequest.City != null && constituencyRequest.City != string.Empty)
                        {
                            constituencyInfo.City = constituencyRequest.City;
                        }

                        // check wheather constituency State  property is null or empty value
                        if (constituencyRequest.State != null && constituencyRequest.State != string.Empty)
                        {
                            constituencyInfo.State = constituencyRequest.State;
                        }                      

                        // update the record in constituency table
                        this.authenticationContext.Constituencies.Update(constituencyInfo);

                        // save the changes into database
                        await this.authenticationContext.SaveChangesAsync();

                        // return the Constituency info
                        return constituencyInfo;
                    }
                    else
                    {
                        throw new Exception("Constituency Record Not Found");
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