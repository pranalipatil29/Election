using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
    public class PartyModel
    {
        [Key]
        public int PartyID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string PartyName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string RegisterBy { get; set; }
    }
}
