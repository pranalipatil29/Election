using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class ConstituencyModel
    {
        [Key]
        public int ConstituencyID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ConstituencyName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string State { get; set; }
    }
}
