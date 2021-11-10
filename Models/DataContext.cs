using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyCongViec.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ConnectionString")
        {
        }
        public DbSet<CongViec> CongViecs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongViec>().HasMany(m => m.Cons)
                                    .WithOptional(m => m.Cha)
                                    .HasForeignKey(m => m.ChaId).WillCascadeOnDelete(false);
        }
    }
}