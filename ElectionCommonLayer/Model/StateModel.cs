using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class StateModel
    {
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Key]
        public int StateID { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }
    }
}
