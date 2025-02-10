
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
      public DbSet<Author> Authors{ get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTag>BlogTags { get; set; }



    }
}
    
    
