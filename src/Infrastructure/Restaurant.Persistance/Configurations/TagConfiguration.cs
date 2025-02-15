using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Models;

namespace Restaurant.Persistence.Configurations
{
    internal class TagConfiguration
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .Property(p => p.Name)
                .HasColumnType("varchar(100)");

            builder
                .HasIndex(p => p.Name)
                .IsUnique();

        }
    }
}
