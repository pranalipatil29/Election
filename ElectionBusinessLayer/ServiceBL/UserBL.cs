using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using ElectionCommonLayer.Model.Vote;
using ElectionRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.ServiceBL
{
   public class UserBL : IUserBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBL"/> class.
        /// </summary>
        /// <param name="adminRL">The user repository layer.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
             
        /// <summary>
        /// Gives the vote.
        /// </summary>
        /// <param name="voteRequest">The vote request.</param>
        /// <returns>
        /// returns true or false indicating operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> GiveVote(VoteRequest voteRequest)
        {
            try
            {
                if (voteRequest.MobileNumber != null)
                {
                    return await this.userRL.GiveVote(voteRequest);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }      
    }
}
