using System.Linq.Expressions;

namespace Pizza.Bll.Helpers
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Sorts an object collection in ascending or descending order by a specified property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The object collection.</param>
        /// <param name="sortBy">The name of the property you want to sort by.</param>
        /// <param name="sortOrder">"desc" for descending ordering (ascending is the default).</param>
        /// <returns>The collection sorted by the parameters.</returns>
        public static IQueryable<T> OrderByCustom<T>(this IQueryable<T> items, string sortBy, string sortOrder)
        {
            var type = typeof(T);
            var expression2 = Expression.Parameter(type, "t");
            var property = type.GetProperty(sortBy);
            var expression1 = Expression.MakeMemberAccess(expression2, property);
            var lambda = Expression.Lambda(expression1, expression2);
            var result = Expression.Call(
                typeof(Queryable),
                sortOrder == "desc" ? "OrderByDescending" : "OrderBy",
                new Type[] { type, property.PropertyType },
                items.Expression,
                Expression.Quote(lambda));

            return items.Provider.CreateQuery<T>(result);
        }

        /// <summary>
        /// Creates a PagedList<T> from an IQueryable by enumerating it asynchronously.
        /// </summary>
        /// <typeparam name="TSource">An IQueryable to create a PagedList<T> from.</typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a PagedList<T> that contains elements from the input sequence.</returns>
        public static async Task<PagedList<TSource>> ToPagedListAsync<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            return await PagedList<TSource>.CreateAsync(source, pageNumber, pageSize);
        }
    }
}
