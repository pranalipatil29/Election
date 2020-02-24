using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectionCommonLayer.Model.Vote
{
   public class VoteRequest
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        [Required]
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the constatuency identifier.
        /// </summary>
        /// <value>
        /// The constatuency identifier.
        /// </value>
        [Required]
        public int ConstatuencyID { get; set; }
    }
}
