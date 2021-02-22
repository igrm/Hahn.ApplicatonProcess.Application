using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IListOfValuesService
    {
        Task<IList<CountryDto>> GetAllCountriesAsync();
        Task<IList<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IList<string>> GetAllTopLevelDomainsAsync();

        void PutAllCountriesInCache();
        void PutAllAllTopLevelDomainsInCache();
    }
}
