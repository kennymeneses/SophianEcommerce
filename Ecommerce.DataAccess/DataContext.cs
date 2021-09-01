using Microsoft.EntityFrameworkCore;
using Ecommerce.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasKey(u => u.userId);

            builder.Entity<User>()
                .HasQueryFilter(u => u.removed == false);

            builder.Entity<User>()
                .HasOne(up => up.passUser)
                .WithOne();
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }

        public DbSet<User> users { get; set; }
    }
}
