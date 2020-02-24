using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class ResultModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The sidentifier.
        /// </value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        [ForeignKey("CandidateModel")]
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        /// <value>
        /// The votes.
        /// </value>
        public int Votes { get; set; }
    }
}
