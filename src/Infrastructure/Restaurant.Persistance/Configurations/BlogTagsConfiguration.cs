using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Configurations
{
    internal class BlogTagsConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(bt => new { bt.BlogId, bt.TagId });
        }
    }
}
