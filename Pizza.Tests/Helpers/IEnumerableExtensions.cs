using Pizza.Bll.Helpers;

namespace Pizza.Tests.Helpers
{
    internal static class IEnumerableExtensions
    {
        /// <summary>
        /// Creates a PagedList<T> from an IEnumerable<T>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The IEnumerable<T> to create a PagedList<T> from.</param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A PagedList<T> that contains elements from the input sequence.</returns>
        public static PagedList<TSource> ToPagedList<TSource>(this IEnumerable<TSource> source, int pageNumber, int pageSize)
        {
            return PagedList<TSource>.Create(source, pageNumber, pageSize);
        }
    }
}
