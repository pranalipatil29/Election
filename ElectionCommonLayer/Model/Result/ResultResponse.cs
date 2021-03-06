﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model.Result
{
    public class ResultResponse
    {
        /// <summary>
        /// Gets or sets the candidate identifier.
        /// </summary>
        /// <value>
        /// The candidate identifier.
        /// </value>
        [ForeignKey("CandidateModel")]
        public int CandidateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        public string CandidateName { get; set; }

        /// <summary>
        /// Gets or sets the name of the party.
        /// </summary>
        /// <value>
        /// The name of the party.
        /// </value>
        public string PartyName { get; set; }

        /// <summary>
        /// Gets or sets the party identifier.
        /// </summary>
        /// <value>
        /// The party identifier.
        /// </value>
        public int PartyID { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        /// <value>
        /// The votes.
        /// </value>
        public int Votes { get; set; }

        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        public int ConstituencyID { get; set; }

        /// <summary>
        /// Gets or sets the name of the constituency.
        /// </summary>
        /// <value>
        /// The name of the constituency.
        /// </value>
        public string ConstituencyName { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int StateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string StateName { get; set; }
    }
}
