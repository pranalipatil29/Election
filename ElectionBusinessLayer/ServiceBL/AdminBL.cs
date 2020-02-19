using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.Admin.Request;
using ElectionCommonLayer.Model.Admin.Respone;
using ElectionRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionBusinessLayer.ServiceBL
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            try
            {
                if(registrationModel != null)
                {
                    return await this.adminRL.Register(registrationModel);
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

        //public Task<AccountResponse> Login(LogInModel logInModel)
        //{

        //}


    }
}
