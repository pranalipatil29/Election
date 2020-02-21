using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.User.Request
{
   public class VoteRequest
    {
        public int ConstituencyID { get; set; }

        public string State { get; set; }

        public int CandidateID { get; set; }
    }
}
