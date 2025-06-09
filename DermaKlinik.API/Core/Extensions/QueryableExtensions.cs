using DermaKlinik.API.Core.Models;
using System.Linq.Expressions;

namespace DermaKlinik.API.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, string direction)
        {
            if (string.IsNullOrEmpty(orderByMember))
                return query;
            
            if (string.IsNullOrEmpty(direction))
                direction = "asc";

            orderByMember = orderByMember.Trim().FirstCharToUpper();
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == "asc" ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int page = 1, int take = 10) where TModel : class
               => take > 0 && page > 0 ? query.Skip((page - 1) * take).Take(take) : query;

        public static PagedList<TModel> PagingWithModel<TModel>(this IQueryable<TModel> query, int page = 1, int take = 10) where TModel : class
        {
            query = query.Paging(page, take);
            var paged = new PagedList<TModel>(query, page, take);
            return paged;
        }

    }

}
