using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
         
            builder.Property(x=>x.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
