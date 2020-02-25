using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCommonLayer.Model.Constituency
{
    public class ConstituenciesListResponse
    {
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
    }
}
