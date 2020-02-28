using ElectionCommonLayer.Model.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.InterfaceBL
{
   public interface IStateBL
    {
        /// <summary>
        /// Registrations the specified admin identifier.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="stateRegistration">The state registration.</param>
        /// <returns>return true or fale indicating operation result</returns>
        Task<bool> Registration(string adminID, StateRegistration stateRegistration);

        /// <summary>
        /// Displays the state.
        /// </summary>
        /// <returns>return states info or null value</returns>
        IList<StateResponse> DisplayStates();

        /// <summary>
        /// Deletes the state.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="StateID">The state identifier.</param>
        /// <returns>return true or fale indicating operation result</returns>
        Task<bool> DeleteStates(string adminID, int StateID);

        /// <summary>
        /// Updates the specified admin identifier.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="stateRequest">The state request.</param>
        /// <returns>return states info or null value</returns>
        Task<StateResponse> Update(string adminID, StateRequest stateRequest);
    }
}
