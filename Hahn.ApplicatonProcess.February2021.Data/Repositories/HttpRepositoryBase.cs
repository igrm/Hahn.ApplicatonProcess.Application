using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public abstract class HttpRepositoryBase
    {
        protected IMemoryCache MemoryCache { get; private set; }
        protected HttpClient HttpClient { get; private set; }
        protected IMapper Mapper { get; private set; }

        public HttpRepositoryBase(IMemoryCache memoryCache, HttpClient client, IMapper mapper)
        {
            MemoryCache = memoryCache;
            HttpClient = client;
            Mapper = mapper;
        }

        protected async Task<IList<T>> Get<T>(Uri uri) where T: class
        {
            IList<T> result;

            if (MemoryCache.TryGetValue(uri, out string cachedText))
            {
                result = ParseRawResponse<T>(cachedText);
            }
            else
            {
                var response = await Fetch(uri);
                var text = await response.Content.ReadAsStringAsync();

                MemoryCache.Set(uri, text);

                result = ParseRawResponse<T>(text);
            }

            return result;
        }

        protected abstract IList<T> ParseRawResponse<T>(string responseText) where T : class;

        protected async virtual Task<HttpResponseMessage> Fetch(Uri uri)
        {
            return await HttpClient.GetAsync(uri);
        }
    }
}
