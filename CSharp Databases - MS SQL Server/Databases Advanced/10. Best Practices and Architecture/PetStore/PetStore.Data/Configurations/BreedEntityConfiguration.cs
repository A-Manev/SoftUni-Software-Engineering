namespace PetStore.Data.Configurations
{
    using PetStore.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetStore.Common;

    public class BreedEntityConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.Property(b => b.Name)
                .HasMaxLength(GlobalConstants.BreedNameMaxLength)
                .IsUnicode(true);
        }
    }
}
