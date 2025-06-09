namespace DermaKlinik.API.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key)
        {
            TValue obj;
            instance.TryGetValue(key, out obj);
            return obj;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key, TValue fallback = default)
        {
            TValue obj;
            if (key == null || !instance.TryGetValue(key, out obj))
                return fallback;
            fallback = obj;
            return fallback;
        }

        public static TValue Get<TValue>(this IDictionary<string, object> instance, string key, TValue fallback = default)
        {
            object obj;
            instance.TryGetValue(key, out obj);
            return (TValue)(obj ?? fallback);
        }

        public static TKey KeyByValue<TKey, TVal>(this Dictionary<TKey, TVal> dict, TVal val)
        {
            TKey key = default;
            foreach (KeyValuePair<TKey, TVal> keyValuePair in dict)
            {
                if (Comparer<TVal>.Default.Compare(keyValuePair.Value, val) == 0)
                {
                    key = keyValuePair.Key;
                    break;
                }
            }
            return key;
        }
    }
}
