namespace PetStore.Data.Configurations
{
    using PetStore.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ClientProductEntityConfiguration : IEntityTypeConfiguration<ClientProduct>
    {
        public void Configure(EntityTypeBuilder<ClientProduct> builder)
        {
            builder
                 .HasKey(cp => new { cp.ClientId, cp.ProductId });
        }
    }
}
