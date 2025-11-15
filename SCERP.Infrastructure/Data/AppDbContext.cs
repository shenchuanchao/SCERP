
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCERP.Domain.Models;
using SCERP.Identity;

namespace SCERP.Infrastructure.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region 系统数据表
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyLog> CurrencyLogs { get; set; }

        #endregion

        #region 基础数据表
        public DbSet<Partner> Partners { get; set; }

        #endregion

        #region 采购模块相关表
        public DbSet<MaterialType> MaterialTypes { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 货币配置
            modelBuilder.Entity<Currency>(entity =>
            {
                //保留三位小数
                entity.Property(c => c.ExchangeRate).HasPrecision(18, 3);
            });

            // 初始种子数据
            SeedDatas.Seed(modelBuilder);

        }

    }
}
