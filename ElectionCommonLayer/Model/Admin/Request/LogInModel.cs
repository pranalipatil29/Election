using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectionCommonLayer.Model.Admin.Request
{
   public class LogInModel
    {
        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
