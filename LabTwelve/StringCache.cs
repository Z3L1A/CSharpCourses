using System.Collections.Generic;
using Functional.Option;

namespace LabTwelve
{
    public class StringCache
    {
        private static StringCache _instance;
        private Dictionary<string, string> _cache;

        private StringCache()
        {
            _cache = new Dictionary<string, string>();
        }

        public static StringCache GetInstance()
        {
            _instance ??= new StringCache();
            return _instance;
        }

        public Option<string> MaybeGet(string key) => 
            _cache.TryGetValue(key, out var value) ? Option<string>.Some(value) : Option<string>.None;

        public bool TryGet(string key, out string value) =>
            _cache.TryGetValue(key, out value);

        public void Add(string key, string value) => 
            _cache.TryAdd(key, value);
    }
}