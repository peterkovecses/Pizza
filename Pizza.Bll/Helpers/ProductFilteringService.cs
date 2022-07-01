using Pizza.Data.Interfaces;

namespace Pizza.Bll.Helpers
{
    public static class ProductFilteringService
    {
        public static IQueryable<T> FilterCategory<T>(this IQueryable<T> products, int? categoryId) where T : IProduct
        {
            if (categoryId == null)
                return products;

            return products.Where(p => p.CategoryId == categoryId);
        }

        public static IQueryable<T> FilterByPrice<T>(this IQueryable<T> products, decimal? minPrice, decimal? maxPrice) where T : IProduct
        {
            if (minPrice == null)
                minPrice = 0;

            if (maxPrice == null)
                return products.Where(p => p.Price >= minPrice);

            return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        public static IQueryable<T> SearchByTerm<T>(this IQueryable<T> products, string searchTerm) where T : IProduct
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                return products.Where(p => p.Name.ToLower().Contains(searchTerm) || (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
            }

            return products;
        }
    }
}
