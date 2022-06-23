using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pizza.Data.Entities
{
    public class Product : EntityBase<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50);

            builder.Property(p => p.Price).HasColumnType("decimal(18,4)");

            builder.HasOne(p => p.Category).WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId).HasPrincipalKey(c => c.Id)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
