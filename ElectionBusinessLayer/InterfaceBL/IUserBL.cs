using ElectionCommonLayer.Model.Candidate;
using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using ElectionCommonLayer.Model.Vote;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.InterfaceBL
{
   public interface IUserBL
    { 
        /// <summary>
       /// Gives the vote.
       /// </summary>
       /// <param name="voteRequest">The vote request.</param>
       /// <returns>returns true or false indicating operation result</returns>
        Task<bool> GiveVote(VoteRequest voteRequest);
    }
}
