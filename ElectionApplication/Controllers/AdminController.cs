using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionBusinessLayer.InterfaceBL;
using ElectionCommonLayer.Model.Admin.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            try
            {

            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
    }
}