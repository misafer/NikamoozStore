﻿using Microsoft.EntityFrameworkCore;

using NikamoozStore.Core.Domain.Categories;
using NikamoozStore.Core.Domain.Orders;
using NikamoozStore.Core.Domain.Products;
using NikamoozStore.Infrastructures.Dal.Categories;
using NikamoozStore.Infrastructures.Dal.Products;

namespace NikamoozStore.Infrastructures.Dal.Commons
{
    public class NikamoozStoreContext : DbContext
    {
        public NikamoozStoreContext(DbContextOptions<NikamoozStoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
