using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pizza.Data.Entities
{
    public class Customer : EntityBase<Customer>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Order> Orders { get; set; }

        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50);
        }
    }
}
