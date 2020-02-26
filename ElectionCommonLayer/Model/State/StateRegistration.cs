using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectionCommonLayer.Model.State
{
  public  class StateRegistration
    {
        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        [Required]
        public string StateName { get; set; }
    }
}
