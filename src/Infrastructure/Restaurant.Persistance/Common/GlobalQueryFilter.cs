
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;


namespace Restaurant.Persistence.Common
{
    internal static class GlobalQueryFilter
    {

        public static void ApplyFilter<T>(this ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(c => c.IsDeleted == false);
        }
        public static void ApplyQueryFilters(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<Menu>();
            modelBuilder.ApplyFilter<Order>();
            modelBuilder.ApplyFilter<OrderItem>();
            modelBuilder.ApplyFilter<Tag>();
            modelBuilder.ApplyFilter<Food>();
            modelBuilder.ApplyFilter<Genre>();
            modelBuilder.ApplyFilter<Author>();
            modelBuilder.ApplyFilter<Category>();
            modelBuilder.ApplyFilter<QR>();
        }
    }
}


