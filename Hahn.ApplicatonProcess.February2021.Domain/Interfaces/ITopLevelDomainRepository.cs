using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface ITopLevelDomainRepository
    {
        Task<IList<string>> GetTopLevelDomainsAsync(Uri uri);
    }
}
