using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SCERP.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 产品配置
            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.HasKey(p => p.Id);
            //    entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            //    entity.Property(p => p.SKU).IsRequired().HasMaxLength(100);
            //    entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //    entity.Property(p => p.OriginalPrice).HasColumnType("decimal(18,2)");
            //    entity.Property(p => p.Brand).HasMaxLength(100);
            //    entity.Property(p => p.VehicleModel).HasMaxLength(100);
            //    entity.Property(p => p.YearRange).HasMaxLength(50);
            //    entity.Property(p => p.ImageUrl).HasMaxLength(500);

            //    entity.HasIndex(p => p.SKU).IsUnique();

            //    // 关系配置
            //    entity.HasOne(p => p.Category)
            //          .WithMany(c => c.Products)
            //          .HasForeignKey(p => p.CategoryId)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});


            // 种子数据
            //SeedData(modelBuilder);
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        //private void SeedData(ModelBuilder modelBuilder)
        //{
        //    // 使用固定的日期时间，而不是动态的 DateTime.UtcNow
        //    var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    // 添加默认分类
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { Id = 1, Name = "发动机部件", Description = "发动机相关配件", SortOrder = 1, CreatedAt = fixedDate }
        //         );

        //}

    }
}
