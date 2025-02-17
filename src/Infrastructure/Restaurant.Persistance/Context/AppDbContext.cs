
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Common;


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
        public DbSet<QR>QRs { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }    
        public DbSet<Food> Foods { get; set; }  
        public DbSet<RestaurantHome> RestaurantHomes { get; set; }
        public DbSet <Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RestaurantTable> Tables { get; set; }    
        public DbSet<Rezervation> Rezervations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilters();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
      
    }
}
    
    
