using Pizza.Data.Interfaces;

namespace Pizza.Bll.Helpers
{
    public static class ProductSortingService
    {
        public static IQueryable<T> OrderProductByCustom<T>(this IQueryable<T> products, string sortBy, string sortOrder) where T : IProduct
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (typeof(IProduct).GetProperty(sortBy) != null)
                    return products.OrderByCustom(sortBy, sortOrder);
            }

            return products;
        }
    }
}
