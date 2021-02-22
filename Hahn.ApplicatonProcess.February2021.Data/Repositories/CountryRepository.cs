using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public class CountryRepository : HttpRepositoryBase, ICountryRepository
    {
        public CountryRepository(IMemoryCache memoryCache, HttpClient client, IMapper mapper) : base(memoryCache, client, mapper)
        {
        }

        public async Task<IList<CountryDto>> GetAllCountriesAsync(Uri uri)
        {
            return await Get<CountryDto>(uri);
        }


        protected override IList<T> ParseRawResponse<T>(string responseText)
        {
            var result = JsonConvert.DeserializeObject<IList<ExpandoObject>>(responseText);
            return result.Select(x => Mapper.Map<T>(x)).ToList();
        }
    }
}
