namespace WebApplication1.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ChatContext : DbContext
    {
        private static ChatContext instance = null;

        public ChatContext() : base("name=ChatContext")
        {
        }

        public static ChatContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChatContext();
                }
                return instance;
            }
        }

        public virtual DbSet<BaiDang> BaiDangs { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Nhom> Nhoms { get; set; }
        public virtual DbSet<TinNhan> TinNhans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiDang>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<BaiDang>()
                .HasMany(e => e.BinhLuans)
                .WithRequired(e => e.BaiDang)
                .HasForeignKey(e => e.IDBaiDang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.TenNguoiBinhLuan)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.BaiDangs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.BinhLuans)
                .WithRequired(e => e.NguoiDung)
                .HasForeignKey(e => e.TenNguoiBinhLuan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.TinNhans)
                .WithOptional(e => e.NguoiDung)
                .HasForeignKey(e => e.NguoiGoi);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.TinNhans1)
                .WithOptional(e => e.NguoiDung1)
                .HasForeignKey(e => e.NguoiNhan);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.Nhoms)
                .WithMany(e => e.NguoiDungs)
                .Map(m => m.ToTable("NhomNguoiDung").MapLeftKey("TenDangNhap").MapRightKey("IdNhom"));

            modelBuilder.Entity<Nhom>()
                .Property(e => e.NguoiTao)
                .IsUnicode(false);

            modelBuilder.Entity<Nhom>()
                .HasMany(e => e.TinNhans)
                .WithRequired(e => e.Nhom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinNhan>()
                .Property(e => e.NguoiGoi)
                .IsUnicode(false);

            modelBuilder.Entity<TinNhan>()
                .Property(e => e.NguoiNhan)
                .IsUnicode(false);
        }
    }
}
