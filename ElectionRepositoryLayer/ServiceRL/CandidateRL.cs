// ******************************************************************************
//  <copyright file="CandidateRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CandidateRL.cs
//  
//     Purpose: Implementing Add, Display, Update & Delete functionality for candidates
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
    using ElectionCommonLayer.Model.Candidate;
    using ElectionRepositoryLayer.Context;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///  this class contains different methods to interact with Candidate table
    /// </summary>
    /// <seealso cref="ElectionRepositoryLayer.InterfaceRL.ICandidateRL" />
    public class CandidateRL : ICandidateRL
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
        /// Initializes a new instance of the <see cref="CandidateRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        /// <param name="userManager">The user manager.</param>
        public CandidateRL(AuthenticationContext authenticationContext, UserManager<ApplicationModel> userManager)
        {
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Adds the candidate.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="candidateModel">The candidate Model.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Candidate VoterID Required
        /// or
        /// Candidate Record Already Exist
        /// or
        /// Not Authorized Account
        /// or
        /// </exception>
        public async Task<bool> AddCandidate(string emailID, CandidateModel candidateModel)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // find record for admin entered voter ID
                    var candidate = this.authenticationContext.Candidates.Where(s => s.VoterID == candidateModel.VoterID).FirstOrDefault();

                    // check wheather candidate record found from candidate table or not
                    if (candidate == null)
                    {
                        if (candidateModel.VoterID != null && candidateModel.VoterID != string.Empty)
                        {
                            // get the required data
                            var data = new CandidateModel()
                            {
                                CandidateName = candidateModel.CandidateName,
                                PartyName = candidateModel.PartyName,
                                VoterID = candidateModel.VoterID,
                                ConstituencyName = candidateModel.ConstituencyName,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            // add data in candidate table
                            this.authenticationContext.Candidates.Add(data);

                            // save the change into database
                            await this.authenticationContext.SaveChangesAsync();

                            return true;
                        }
                        else
                        {
                            throw new Exception("Candidate VoterID Required");
                        }                      
                    }
                    else
                    {
                        throw new Exception("Candidate Record Already Exist");
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
        /// Displays the candidate records.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Not Authorized Account
        /// or
        /// </exception>
        public IList<CandidateResponse> DisplayCandidateRecords(string emailID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the record from Cadidates table
                    var data = this.authenticationContext.Candidates.Select(s => s);

                    // create variable to store list of Candidate info
                    var list = new List<CandidateResponse>();

                    // check wheather the data contains any null value or not
                    if (data != null)
                    {
                        // iterates the loop for each record
                        foreach (var record in data)
                        {
                            var candidate = new CandidateResponse()
                            {
                               CandidateID = record.CandidateID,
                               CandidateName = record.CandidateName,
                               ConstituencyName = record.ConstituencyName,
                               PartyName = record.PartyName,
                               VoterID = record.VoterID
                            };

                            // add the record in list
                            list.Add(candidate);
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
        /// Deletes the candidate record.
        /// </summary>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="emailID">The email identifier.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Record Not Found
        /// or
        /// UnAuthorized Account
        /// or
        /// </exception>
        public async Task<bool> DeleteCandidate(int candidateID, string emailID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the candidate data
                    var record = this.authenticationContext.Candidates.Where(s => s.CandidateID == candidateID).FirstOrDefault();

                    // check wheather any record for candidate found or not
                    if (record != null)
                    {
                        // remove the record from candidate table
                        this.authenticationContext.Candidates.Remove(record);

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
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> DeleteBulk(BulkCandidateRequest bulkRequest, string adminID)
        {
            try
            {
                // flag variable is used to indicate operation result
                bool flag = false;

                // iterates the loop for each record
                foreach (var record in bulkRequest.CandidateID)
                {
                    // get the delete operation result
                    var result = await this.DeleteCandidate(record, adminID);

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
        /// <param name="candidateRequest">The candidate request.</param>
        /// <param name="candidateID">The candidate identifier.</param>
        /// <param name="adminID">The admin identifier.</param>
        /// <returns>
        /// return true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Constituency Name Not Exist
        /// or
        /// Party Not Registred
        /// or
        /// Party Record Not Found
        /// or
        /// UnAuthorized Account
        /// or
        /// </exception>
        public async Task<CandidateModel> UpdateInfo(CandidateRequest candidateRequest, int candidateID, string adminID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the candidate data
                    var candidateInfo = this.authenticationContext.Candidates.Where(s => s.CandidateID == candidateID).FirstOrDefault();

                    // check wheather any record for candidate found or not
                    if (candidateInfo != null)
                    {
                        // modify the date value
                        candidateInfo.ModifiedDate = DateTime.Now;

                        // check wheather candidate name  property is null or empty value
                        if (candidateRequest.CandidateName != null && candidateRequest.CandidateName != string.Empty)
                        {
                            candidateInfo.CandidateName = candidateRequest.CandidateName;
                        }

                        // check wheather Constituency Name  By property contains any null or empty value
                        if (candidateRequest.ConstituencyName != null && candidateRequest.ConstituencyName != string.Empty)
                        {
                            var constituency = this.authenticationContext.Constituencies.Where(s => s.ConstituencyName == candidateRequest.ConstituencyName).FirstOrDefault();

                            if (constituency != null)
                            {
                                candidateInfo.ConstituencyName = candidateRequest.ConstituencyName;
                            }
                            else
                            {
                                throw new Exception("Constituency Name Not Exist");
                            }
                        }

                        // check wheather Party Name  By property contains any null or empty value
                        if (candidateRequest.PartyName != null && candidateRequest.PartyName != string.Empty)
                        {
                            var party = this.authenticationContext.Parties.Where(s => s.PartyName == candidateRequest.PartyName).FirstOrDefault();

                            if (party != null)
                            {
                                candidateInfo.PartyName = candidateRequest.PartyName;
                            }
                            else
                            {
                                throw new Exception("Party Not Registred");
                            }                          
                        }
                                               
                        // update the record in parties table
                        this.authenticationContext.Candidates.Update(candidateInfo);

                        // save the changes into database
                        await this.authenticationContext.SaveChangesAsync();

                        // return the party info
                        return candidateInfo;
                    }
                    else
                    {
                        throw new Exception("Party Record Not Found");
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