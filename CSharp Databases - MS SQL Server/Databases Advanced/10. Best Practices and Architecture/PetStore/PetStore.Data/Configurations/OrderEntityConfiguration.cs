namespace PetStore.Data.Configurations
{
    using PetStore.Models;
    using PetStore.Common;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                 .Property(o => o.Town)
                 .HasMaxLength(GlobalConstants.TownNameMaxLength)
                 .IsUnicode(true);

            builder
                .Property(o => o.Address)
                .HasMaxLength(GlobalConstants.AddressMaxLength)
                .IsUnicode(true);

            builder
                .Ignore(o => o.TotalPrice);
        }
    }
}
