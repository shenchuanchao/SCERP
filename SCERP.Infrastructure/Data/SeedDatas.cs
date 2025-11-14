using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCERP.Domain.SeedData;

namespace SCERP.Infrastructure.Data
{
    /// <summary>
    /// 种子数据类
    /// </summary>
    public class SeedDatas
    {
        //创建一个添加种子数据仓库方法
        public static void Seed(ModelBuilder modelBuilder)
        {
            //物料类型种子数据
            modelBuilder.Entity<Domain.Models.MaterialType>().HasData(MaterialTypeList.Data());

     
        }




    }
}
