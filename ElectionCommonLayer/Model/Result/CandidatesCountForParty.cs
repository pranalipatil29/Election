using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.Result
{
   public class CandidatesCountForParty
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
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }
    }
}
