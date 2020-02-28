using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.State;
using ElectionRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.ServiceBL
{
   public class StateBL :IStateBL
    {
        /// <summary>
        /// The state repository layer interface
        /// </summary>
        private readonly IStateRL stateRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateBL"/> class.
        /// </summary>
        /// <param name="stateRL">The state repository layer.</param>
        public StateBL(IStateRL stateRL)
        {
            this.stateRL = stateRL;
        }

        /// <summary>
        /// Registrations the specified email identifier.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="stateRegistration">The state registration.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        /// <exception cref="Exception">return exception</exception>
        public async Task<bool> Registration(string adminID, StateRegistration stateRegistration)
        {
            try
            {
                // check whether admin entered any null value or not
                if (stateRegistration != null)
                {
                    // return the operation result
                    return await this.stateRL.Registration(adminID, stateRegistration);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the states.
        /// </summary>
        /// <returns>return states info or null value</returns>
        /// <exception cref="Exception"></exception>
        public IList<StateResponse> DisplayStates()
        {
            try
            {
                return this.stateRL.DisplayStates();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the state.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="stateID">The state identifier.</param>
        /// <returns>return true or fale indicating operation result</returns>
        /// <exception cref="Exception">
        /// State ID is Required
        /// or
        /// </exception>
        public async Task<bool> DeleteStates(string adminID, int stateID)
        {
            try
            {
                if (stateID > 0)
                {
                    return await this.stateRL.DeleteState(adminID, stateID);
                }
                else
                {
                    throw new Exception("State ID is Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Upadates the specified admin identifier.
        /// </summary>
        /// <param name="adminID">The admin identifier.</param>
        /// <param name="stateRequest">The state request.</param>
        /// <returns>return states info or null value</returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<StateResponse> Update(string adminID, StateRequest stateRequest)
        {
            try
            {
                // check wheather admin entered correct State info
                if (stateRequest.StateID > 0)
                {
                    return await this.stateRL.Update(adminID, stateRequest);
                }
                else
                {
                    throw new Exception("Please provide valid State ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
