using Microsoft.Extensions.Configuration;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Hahn.ApplicatonProcess.February2021.Data.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfiguration _configuration;
        public SettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri GetRestCountriesUri()
        {
            return new Uri(_configuration.GetSection("Integrations").GetSection("CountryListEnpoint").Value);
        }

        public string[] GetExcludeCountries()
        {
            return _configuration.GetSection("Integrations")
                                 .GetSection("ExcludeList")
                                 .GetChildren()
                                 .Select(x => x.Value)
                                 .ToArray();
        }

        public Uri GetTopLevelDomainsUri()
        {
            return new Uri(_configuration.GetSection("Integrations").GetSection("TopLevelDomainEndpoint").Value);
        }

        public CultureInfo GetCurrentCulture()
        {
            return new CultureInfo(_configuration.GetSection("Integrations").GetSection("CurrentCulture").Value);
        }
    }
}
