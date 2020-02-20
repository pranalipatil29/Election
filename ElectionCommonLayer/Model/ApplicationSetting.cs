using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class ApplicationSetting
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        /// <value>
        /// The JWT secret.
        /// </value>
        public string JWTSecret { get; set; }

        /// <summary>
        /// Gets or sets the client u rl.
        /// </summary>
        /// <value>
        /// The client u rl.
        /// </value>
        public string ClientURl { get; set; }

        /// <summary>
        /// Gets or sets the name of the cloud.
        /// </summary>
        /// <value>
        /// The name of the cloud.
        /// </value>
        public string CloudName { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string APIkey { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>
        /// The API secret.
        /// </value>
        public string APISecret { get; set; }

    }
}
