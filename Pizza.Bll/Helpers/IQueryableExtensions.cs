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
    }
}
