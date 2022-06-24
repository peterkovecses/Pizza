using Pizza.Data.Entities;

namespace Pizza.Bll.Helpers
{
    public static class ProductFilteringService
    {
        public static IQueryable<T> FilterByPrice<T>(this IQueryable<T> products, decimal? minPrice, decimal? maxPrice) where T : Product
        {
            if (minPrice == null)
                minPrice = 0;

            if (maxPrice == null)
                return products.Where(p => p.Price >= minPrice);

            return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        public static IQueryable<T> SearchByTerm<T>(this IQueryable<T> products, string searchTerm) where T : Product
        {
            if (!string.IsNullOrEmpty(searchTerm))
                return products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            return products;
        }
    }
}
