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
    using ElectionCommonLayer.Model;
    using ElectionCommonLayer.Model.Candidate;
    using ElectionRepositoryLayer.Context;
    using ElectionRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Identity;
    using System;
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
        /// Initializes a new instance of the <see cref="PartiesRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        public CandidateRL(AuthenticationContext authenticationContext, UserManager<ApplicationModel> userManager)
        {
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }

        public async Task<bool> AddCandidate(string emailID, CandidateRequest candidateRequest)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // find record for admin entered voter ID
                    var candidate = this.authenticationContext.Candidates.Where(s => s.VoterID == candidateRequest.VoterID).FirstOrDefault();

                    // check wheather candidate record found from candidate table or not
                    if (candidate == null)
                    {
                        // get the required data
                        var data = new CandidateModel()
                        {
                            CandidateName = candidateRequest.CandidateName,
                            PartyName = candidateRequest.PartyName,
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
    }
}
