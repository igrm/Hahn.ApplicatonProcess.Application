using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.API
{
    public class GetTopLevelDomainsResponse
    {
        public GetTopLevelDomainsResponse()
        {
            TopLevelDomains = new List<string>();
        }

        /// <summary>
        /// list of top level domains
        /// </summary>
        /// <example>[".com",".net"]</example>
        public IList<String> TopLevelDomains { get; set; }
    }
}
