using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pizza.Data.Entities
{
    public class Order : EntityBase<Order>
    {
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }

        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Customer).WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId).HasPrincipalKey(c => c.Id)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
