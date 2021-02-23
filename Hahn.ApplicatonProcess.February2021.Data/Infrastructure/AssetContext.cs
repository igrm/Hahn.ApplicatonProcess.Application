using Hahn.ApplicatonProcess.February2021.Domain.Models.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.February2021.Data.Infrastructure
{
    public class AssetContext : DbContext
    {
        public DbSet<Asset> Asset { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
            Asset = Set<Asset>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().Property<byte[]>("RowVersion").IsRowVersion();
        }
    }
}
