using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.Admin.Respone
{
   public class PartywiseResultResponse
    {
        /// <summary>
        /// Gets or sets the party identifier.
        /// </summary>
        /// <value>
        /// The party identifier.
        /// </value>
        public int PartyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the won.
        /// </summary>
        /// <value>
        /// The won.
        /// </value>
        public int Won { get; set; }

        /// <summary>
        /// Gets or sets the loss.
        /// </summary>
        /// <value>
        /// The loss.
        /// </value>
        public int Loss { get; set; }
    }
}
