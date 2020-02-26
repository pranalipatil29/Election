using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.State
{
   public class StateResponse
    {
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
