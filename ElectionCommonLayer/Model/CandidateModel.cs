using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class CandidateModel
    {
        [Key]
        public int CandidateID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string CandidateName { get; set; }             

        [ForeignKey("ConstituencyModel")]
        public string ConstituencyName { get; set; }

        [ForeignKey("PartyName")]
        public string PartyName { get; set; }
    }
}
