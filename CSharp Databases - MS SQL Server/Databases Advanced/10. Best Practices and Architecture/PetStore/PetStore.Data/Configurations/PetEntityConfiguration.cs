namespace PetStore.Data.Configurations
{
    using PetStore.Models;
    using PetStore.Common;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder
                 .Property(p => p.Name)
                 .HasMaxLength(GlobalConstants.PetNameMaxLength)
                 .IsUnicode(true);
        }
    }
}
