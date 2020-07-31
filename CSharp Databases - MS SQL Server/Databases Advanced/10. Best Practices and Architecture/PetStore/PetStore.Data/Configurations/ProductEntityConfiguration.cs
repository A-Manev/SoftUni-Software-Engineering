namespace PetStore.Data.Configurations
{
    using PetStore.Models;
    using PetStore.Common;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasAlternateKey(p => p.Name);

            builder
                .Property(p => p.Name)
                .HasMaxLength(GlobalConstants.ProductNameMaxLength)
                .IsUnicode(true);
        }
    }
}
