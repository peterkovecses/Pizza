using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pizza.Data.Entities
{
    public class ProductOrder : EntityBase<ProductOrder>
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public override void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasOne(po => po.Product).WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId).HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(po => po.Order).WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderId).HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
