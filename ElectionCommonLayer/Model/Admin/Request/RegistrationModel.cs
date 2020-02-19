using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectionCommonLayer.Model.Admin.Request
{
   public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string VoterID { get; set; }

        public string ProfilePicture { get; set; }
    }
}
