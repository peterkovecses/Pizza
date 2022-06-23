using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pizza.Data.Entities
{
    public class Category : EntityBase<Category>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;


        public ICollection<Product> Products { get; set; }

        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50);
        }

        public static readonly byte ChicagoDeepDishPizza = 1;
        public static readonly byte ChicagoThinCrustPizza = 2;
        public static readonly byte Beer = 3;
        public static readonly byte Wine = 4;
    }
}