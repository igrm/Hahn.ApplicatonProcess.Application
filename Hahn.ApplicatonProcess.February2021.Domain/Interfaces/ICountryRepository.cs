using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<IList<CountryDto>> GetAllCountriesAsync(Uri uri);
    }
}
