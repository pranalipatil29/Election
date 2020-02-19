using ElectionCommonLayer.Model.Admin.Request;
using ElectionCommonLayer.Model.Admin.Respone;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.InterfaceRL
{
    public interface IAdminRL
    {
        Task<bool> Register(RegistrationModel registrationModel);

       // Task<AccountResponse> Login(LogInModel logInModel);
    }
}
