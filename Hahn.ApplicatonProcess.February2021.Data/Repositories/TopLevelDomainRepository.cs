using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public class TopLevelDomainRepository : HttpRepositoryBase, ITopLevelDomainRepository
    {
        public TopLevelDomainRepository(IMemoryCache memoryCache, HttpClient client, IMapper mapper) 
                        : base(memoryCache, client, mapper)
        {

        }

        public async Task<IList<string>> GetTopLevelDomainsAsync(Uri uri)
        {
            return await Get<string>(uri);
        }

        protected override IList<T> ParseRawResponse<T>(string responseText)
        {
            var result = (IList<T>) responseText.Split("\n")
                                                .Skip(1)
                                                .Select(x => (x != null ? $".{ x.ToLower()}": String.Empty) as T)
                                                .ToList();
            return result;
        }
    }
}
