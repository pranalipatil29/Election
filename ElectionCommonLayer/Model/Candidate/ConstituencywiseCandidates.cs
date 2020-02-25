using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.Candidate
{
    public class ConstituencywiseCandidates
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        public string CandidateName { get; set; }

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
    }
}
