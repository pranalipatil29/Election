using ElectionCommonLayer.Model.User.Request;
using ElectionCommonLayer.Model.User.Response;
using ElectionCommonLayer.Model.Vote;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.InterfaceRL
{
    public interface IUserRL
    {
        /// <summary>
        /// Gives the vote.
        /// </summary>
        /// <param name="voteRequest">The vote request.</param>
        /// <returns>returns true or false indicating operation result</returns>
        Task<bool> GiveVote(VoteRequest voteRequest);       
    }
}
