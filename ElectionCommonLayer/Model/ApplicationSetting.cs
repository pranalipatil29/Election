using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class ApplicationSetting
    {
        public string JWTSecret { get; set; }

        public string ClientURl { get; set; }
    }
}
