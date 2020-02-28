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
        /// <param name="candidateRequest">The candidate Request.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Candidate VoterID Required
        /// or
        /// Candidate Record Already Exist
        /// or
        /// Not Authorized Account
        /// or
        /// </exception>
        public async Task<bool> AddCandidate(string emailID, CandidateRequest candidateRequest)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // find record for candidate
                    var candidate = this.authenticationContext.Candidates.Where(s => s.MobileNumber == candidateRequest.MobileNumber && s.ConstituencyID == candidateRequest.ConstituencyID && s.PartyID == candidateRequest.PartyID).FirstOrDefault();

                    // check wheather candidate record found from candidate table or not
                    if (candidate == null)
                    {
                        if (candidateRequest.MobileNumber != null && candidateRequest.MobileNumber != string.Empty)
                        {
                            // get candidate details from Account table
                            var user = this.authenticationContext.AccountTable.Where(s => s.MobileNumber == candidateRequest.MobileNumber).FirstOrDefault();

                            // check wheather user is exit or not
                            if (user != null)
                            {
                                // get constituency details from constituency table
                                var constituency = this.authenticationContext.Constituencies.Where(s => s.ConstituencyID == candidateRequest.ConstituencyID && s.StateID == candidateRequest.StateID).FirstOrDefault();

                                // get party details from Party table
                                var party = this.authenticationContext.Parties.Where(s => s.PartyID == candidateRequest.PartyID).FirstOrDefault();

                                // get state details
                                var state = this.authenticationContext.States.Where(s => s.StateID == candidateRequest.StateID).FirstOrDefault();

                                var userName = user.FirstName + " " + user.LastName;

                                if (state != null)
                                {
                                    // check wheather constituency is registered or not
                                    if (constituency != null)
                                    {
                                        // check wheather Party is registered or not
                                        if (party != null)
                                        {
                                            // get the required data
                                            var data = new CandidateModel()
                                            {
                                                CandidateName = userName,
                                                PartyID = candidateRequest.PartyID,
                                                PartyName = party.PartyName,
                                                MobileNumber = candidateRequest.MobileNumber,
                                                ConstituencyID = candidateRequest.ConstituencyID,
                                                StateID = state.StateID,
                                                StateName = state.StateName,
                                                ConstituencyName = constituency.ConstituencyName,
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
                                            throw new Exception("Party Does Not Exist");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Constituency Name not Exist");
                                    }
                                }
                                else
                                {
                                    throw new Exception("State record not found");
                                }                                
                            }
                            else
                            {
                                throw new Exception("Candidate not found");
                            }
                        }
                        else
                        {
                            throw new Exception("Candidate Mobile Number Required");
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
                                ConstituencyID = record.ConstituencyID,
                                ConstituencyName = record.ConstituencyName,
                                StateID = record.StateID,
                                StateName = record.StateName,
                                PartyID = record.PartyID,
                                PartyName = record.PartyName,
                                MobileNumber = record.MobileNumber
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
        public async Task<CandidateModel> UpdateInfo(UpdateRequest updateRequest, int candidateID, string adminID)
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
                        var account = this.authenticationContext.AccountTable.Where(s => s.MobileNumber == candidateInfo.MobileNumber).FirstOrDefault();

                        // modify the date value
                        candidateInfo.ModifiedDate = DateTime.Now;

                        if (updateRequest.FirstName != null || updateRequest.LastName != null)
                        {
                            var name = string.Empty;

                            // check request for only changing first name
                            if (updateRequest.FirstName != null && updateRequest.FirstName != string.Empty && updateRequest.LastName == null || updateRequest.LastName == string.Empty)
                            {
                                account.FirstName = updateRequest.FirstName;
                                name = updateRequest.FirstName + " " + account.LastName;
                                candidateInfo.CandidateName = name;
                                
                                this.authenticationContext.AccountTable.Update(account);
                                await this.authenticationContext.SaveChangesAsync();                              
                            }

                            // check request for only changing last name
                            if (updateRequest.FirstName == null || updateRequest.FirstName == string.Empty && updateRequest.LastName != null && updateRequest.LastName != string.Empty)
                            {
                                account.LastName = updateRequest.LastName;
                                name = account.FirstName + " " + updateRequest.LastName;
                                candidateInfo.CandidateName = name;

                                this.authenticationContext.AccountTable.Update(account);
                                await this.authenticationContext.SaveChangesAsync();
                            }

                            // check wheather user wants to update full name
                            if (updateRequest.FirstName != null && updateRequest.FirstName != string.Empty && updateRequest.LastName != null && updateRequest.LastName != string.Empty)
                            {
                                name = updateRequest.FirstName + " " + updateRequest.LastName;
                                candidateInfo.CandidateName = name;

                                account.FirstName = updateRequest.FirstName;
                                account.LastName = updateRequest.LastName;

                                this.authenticationContext.AccountTable.Update(account);
                                await this.authenticationContext.SaveChangesAsync();
                            }
                        }                       

                        // check wheather Constituency ID  By property contains any null or empty value
                        if (updateRequest.ConstituencyID > 0)
                        {
                            // get candidates details from cadidate table
                            var candidates = this.authenticationContext.Candidates.Where(s => s.ConstituencyID == updateRequest.ConstituencyID && updateRequest.PartyID == s.PartyID).FirstOrDefault();

                            if (candidates == null)
                            {
                                var constituency = this.authenticationContext.Constituencies.Where(s => s.ConstituencyID == updateRequest.ConstituencyID).FirstOrDefault();

                                if (constituency != null)
                                {
                                    candidateInfo.ConstituencyID = updateRequest.ConstituencyID;
                                    candidateInfo.ConstituencyName = constituency.ConstituencyName;
                                    candidateInfo.StateID = constituency.StateID;
                                    candidateInfo.StateName = constituency.StateName;
                                }
                                else
                                {
                                    throw new Exception("Constituency record Not Exist");
                                }
                            }
                            else
                            {
                                throw new Exception("Candidate Already regiter for this Constituency by user Party");
                            }                          
                        }

                        // check wheather Party ID By property contains any null or empty value
                        if (updateRequest.PartyID > 0)
                        {
                            var party = this.authenticationContext.Parties.Where(s => s.PartyID == updateRequest.PartyID).FirstOrDefault();
                            // get candidates details from cadidate table
                            var candidates = this.authenticationContext.Candidates.Where(s => s.ConstituencyID == updateRequest.ConstituencyID && updateRequest.PartyID == s.PartyID).FirstOrDefault();

                            if (candidates == null)
                            {
                                if (party != null)
                                {
                                    candidateInfo.PartyID = updateRequest.PartyID;
                                    candidateInfo.PartyName = party.PartyName;
                                }
                                else
                                {
                                    throw new Exception("Party Not Registred");
                                }
                            }
                            else
                            {
                                throw new Exception("Candidate Already regiter for this Constituency by user Party");
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

        /// <summary>
        /// Gets the constituencywise candidates.
        /// </summary>
        /// <param name="constituencyID">The constituency identifier.</param>
        /// <param name="stateID">The state identifier.</param>
        /// <returns>
        /// returns the candidates list or null value
        /// </returns>
        /// <exception cref="Exception"></exception>
        public IList<ConstituencywiseCandidates> GetConstituencywiseCandidates(int constituencyID, int stateID)
        {
            try
            {
                var list = new List<ConstituencywiseCandidates>();

                var candidates = this.authenticationContext.Candidates.Where(s => s.ConstituencyID == constituencyID && s.StateID == stateID);

                if (candidates != null)
                {
                    foreach (var candidate in candidates)
                    {
                        var data = new ConstituencywiseCandidates()
                        {
                            CandidateID = candidate.CandidateID,
                            CandidateName = candidate.CandidateName,
                            PartyID = candidate.PartyID,
                            PartyName = candidate.PartyName
                        };

                        list.Add(data);
                    }

                    return list;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}