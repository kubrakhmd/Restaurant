using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Persistence.Configurations
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
             builder
               .Property(p => p.Name)
               .IsRequired()
            .HasMaxLength(100);


            builder
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(6,2)");



          
        }
    }
}
