using Pizza.Data.Entities;

namespace Pizza.Bll.Helpers
{
    public static class ProductSortingService
    {
        public static IQueryable<T> OrderProductByCustom<T>(this IQueryable<T> products, string sortBy, string sortOrder) where T : Product
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (typeof(Product).GetProperty(sortBy) != null)
                    return products.OrderByCustom(sortBy, sortOrder);
            }

            return products;
        }
    }
}
