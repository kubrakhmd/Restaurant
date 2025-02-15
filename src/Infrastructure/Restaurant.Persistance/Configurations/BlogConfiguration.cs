using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Article)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(b => b.Title)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}
