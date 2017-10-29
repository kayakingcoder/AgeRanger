using System;

namespace AgeRanger.Core
{
    //Generic filter (arguments) for API get methods
    public class ListFilter
    {
        /// <summary>
        /// Total number of RECORDS (not pages) to skip for paging
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// Number of records to return per query/page
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// Search string
        /// </summary>
        public string q { get; set; }
    }
}
