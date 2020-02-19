﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class ApplicationModel : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ProfilePicture { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string UserType { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string VoterID { get; set; }
    }
}
