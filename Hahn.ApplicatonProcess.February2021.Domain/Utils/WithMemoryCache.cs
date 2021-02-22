using Microsoft.Extensions.Caching.Memory;

namespace Hahn.ApplicatonProcess.February2021.Domain.Utils
{
    public class WithMemoryCache<T> where T: new()
    {
        public WithMemoryCache()
        {
            Instance = new T();
        }

        public IMemoryCache? MemoryCache { get; set; }
        public T Instance { get; set; }
    }
}
