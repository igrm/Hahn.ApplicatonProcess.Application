using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Hahn.ApplicatonProcess.February2021.Domain.Models.DTO
{
    public class CountryDto
    {
        public CountryDto()
        {
            Alpha3Code = String.Empty;
            Name = String.Empty;
            _translations = new Dictionary<string, string>();
            TopLevelDomains = new List<string>();
        }

        private IDictionary<string, string> _translations;

        /// <summary>
        /// The country name
        /// </summary>
        /// <example>Belgium</example>
        public string Name { get; set; }

        /// <summary>
        /// The country 3 letter code in upper case
        /// </summary>
        /// <example>BEL</example>
        public string Alpha3Code { get; set; }

        /// <summary>
        /// The list of top level domains of the country
        /// </summary>
        /// <example>[".be"]</example>
        public IList<string> TopLevelDomains { get; set; }

        /// <summary>
        /// The list of country name translations
        /// </summary>
        /// <example>{"en": "Belgium"}</example>
        public IDictionary<string, string> Translations
        {
            get
            {
                var translation = new Dictionary<string, string>() { { "en", Name } };
                translation = translation.Concat(_translations).ToDictionary(x => x.Key, y => y.Value);
                return translation;
            }
            set
            {
                _translations = value;
            }
        }
    }
}
