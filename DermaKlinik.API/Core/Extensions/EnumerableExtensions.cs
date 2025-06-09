using System.Data;

namespace DermaKlinik.API.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<List<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                   .Select((x, i) => new { Index = i, Value = x })
                   .GroupBy(x => x.Index / chunkSize)
                   .Select(x => x.Select(v => v.Value).ToList())
                   .ToList();
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source)
                action(obj);
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int num = 0;
            foreach (T obj in source)
                action(obj, num++);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source)
                action(obj);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int num = 0;
            foreach (T obj in source)
                action(obj, num++);
        }


        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, params T[] second) => first.Concat(second);

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> first, params T[] second) => second.Concat(first);

        public static string Join<T>(this IEnumerable<T> source, string seperator) => string.Join(seperator, source);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}
