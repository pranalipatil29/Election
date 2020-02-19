using ElectionCommonLayer.Model;
using ElectionCommonLayer.Model.Admin.Request;
using ElectionRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElectionRepositoryLayer.ServiceRL
{
    public class AdminRL : IAdminRL
    {
        private readonly UserManager<ApplicationModel> userManager;

        private readonly SignInManager<ApplicationModel> signInManager;

        private readonly ApplicationSetting applicationSettings;

        public AdminRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSettings = appSettings.Value;
        }

        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            var user = await this.userManager.FindByEmailAsync(registrationModel.EmailID);

            if (user == null)
            {
                var data = new ApplicationModel()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Email = registrationModel.EmailID,
                    VoterID = registrationModel.VoterID,
                    ProfilePicture = registrationModel.ProfilePicture,
                    UserType = "Admin"
                };

                var result = await this.userManager.CreateAsync(data, registrationModel.Password);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
