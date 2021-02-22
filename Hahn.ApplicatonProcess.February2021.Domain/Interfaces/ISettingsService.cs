using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface ISettingsService
    {
        Uri GetRestCountriesUri();
        string[] GetExcludeCountries();

        Uri GetTopLevelDomainsUri();

        CultureInfo GetCurrentCulture();
    }
}
