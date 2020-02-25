using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.Result
{
  public  class CandidateResultByConstituency
    {
        /// <summary>
        /// Gets or sets the constituency identifier.
        /// </summary>
        /// <value>
        /// The constituency identifier.
        /// </value>
        public int ConstituencyID { get; set; }

        /// <summary>
        /// Gets or sets the candidte result by contituency.
        /// </summary>
        /// <value>
        /// The candidte result by contituency.
        /// </value>
        public ResultResponse CandidtesResultByContituency { get; set; }
    }
}
