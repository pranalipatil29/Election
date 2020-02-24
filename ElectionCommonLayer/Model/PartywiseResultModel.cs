using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionCommonLayer.Model
{
   public class PartywiseResultModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the party identifier.
        /// </summary>
        /// <value>
        /// The party identifier.
        /// </value>
        [ForeignKey("PartyModel")]
        public int PartyID { get; set; }
               
        /// <summary>
        /// Gets or sets the won.
        /// </summary>
        /// <value>
        /// The won.
        /// </value>
        public int Won { get; set; }

        /// <summary>
        /// Gets or sets the loss.
        /// </summary>
        /// <value>
        /// The loss.
        /// </value>
        public int Loss { get; set; }
    }
}
