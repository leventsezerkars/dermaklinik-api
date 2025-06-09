namespace DermaKlinik.API.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> initial, IEnumerable<T> other)
        {
            if (other == null)
                return;
            if (initial is List<T> objList)
                objList.AddRange(other);
            else
                other.Each(x => initial.Add(x));
        }
    }
}
