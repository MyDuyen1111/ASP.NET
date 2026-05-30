using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OnTX2_1.Models
{
    public partial class TraiCayContext : DbContext
    {
        public TraiCayContext()
            : base("name=TraiCayContext")
        {
        }

        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>()
                .Property(e => e.DonGia)
                .HasPrecision(10, 2);
        }
    }
}
