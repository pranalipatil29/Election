using ElectionCommonLayer.Model;
using ElectionCommonLayer.Model.State;
using ElectionRepositoryLayer.Context;
using ElectionRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.ServiceRL
{
    public class StateRL : IStateRL
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
        /// Initializes a new instance of the <see cref="StateRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        /// <param name="userManager">The user manager.</param>
        public StateRL(AuthenticationContext authenticationContext, UserManager<ApplicationModel> userManager)
        {
            this.userManager = userManager;
            this.authenticationContext = authenticationContext;
        }


        public async Task<bool> Registration(string adminID, StateRegistration stateRegistration)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == adminID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // find record for admin entered State Name
                    var state = this.authenticationContext.States.Where(s => s.StateName == stateRegistration.StateName).FirstOrDefault();

                    // check wheather state record found from States table or not
                    if (state == null)
                    {
                        // get the required data
                        var data = new StateModel()
                        {
                            StateName = stateRegistration.StateName,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };

                        // add data in State table
                        this.authenticationContext.States.Add(data);

                        // save the change into database
                        await this.authenticationContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        throw new Exception("State Record Already Exist");
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

        public IList<StateResponse> DisplayStates()
        {
            try
            {
                // get the record from States table
                var data = this.authenticationContext.States.Select(s => s);

                // create variable to store list of States info
                var list = new List<StateResponse>();

                // check wheather the data contains any null value or not
                if (data != null)
                {
                    // iterates the loop for each record
                    foreach (var record in data)
                    {
                        var state = new StateResponse()
                        {
                            StateID = record.StateID,
                            StateName = record.StateName
                        };

                        // add the record in list
                        list.Add(state);
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

        public async Task<bool> DeleteState(string emailID, int stateID)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the State data
                    var record = this.authenticationContext.States.Where(s => s.StateID == stateID).FirstOrDefault();

                    // check wheather any record for State found or not
                    if (record != null)
                    {
                        // remove the record from States table
                        this.authenticationContext.States.Remove(record);

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

        public async Task<StateResponse> Update(string emailID, StateRequest stateRequest)
        {
            try
            {
                // get the admin data
                var adminData = this.userManager.Users.Where(s => s.Email == emailID && s.UserType == "Admin").FirstOrDefault();

                // check wheather admin record is found or not
                if (adminData != null)
                {
                    // get the State data
                    var stateInfo = this.authenticationContext.States.Where(s => s.StateID == stateRequest.StateID).FirstOrDefault();

                    // check wheather any record for State found or not
                    if (stateInfo != null)
                    {
                        // modify the date value
                        stateInfo.ModifiedDate = DateTime.Now;

                        // check wheather State name  property is null or empty value
                        if (stateRequest.StateName != null && stateRequest.StateName != string.Empty)
                        {
                            stateInfo.StateName = stateRequest.StateName;
                        }
                        else
                        {
                            throw new Exception("State Name Required");
                        }

                        // update the record in State table
                        this.authenticationContext.States.Update(stateInfo);

                        // save the changes into database
                        await this.authenticationContext.SaveChangesAsync();

                        var data = new StateResponse()
                        {
                            StateID = stateInfo.StateID,
                            StateName = stateInfo.StateName
                        };

                        // return the State data
                        return data;
                    }
                    else
                    {
                        throw new Exception("state Record Not Found");
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
