using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain.Models;

namespace SCERP.Domain.SeedData
{
    /// <summary>
    /// 物料类型种子数据
    /// </summary>
    public class MaterialTypeList
    {
        /// <summary>
        /// 数据
        /// </summary>
        /// <returns></returns>
        public static List<MaterialType> Data()
        {
            var list = new List<MaterialType>();
            list.Add(new MaterialType
            {
                Id = "b4236b8c-fe68-4e5b-8ee4-59f3fb5ba35b",
                EnCode = "MT001",
                FullName = "板材"
            });
            list.Add(new MaterialType
            {
                Id = "fa635b68-9fe5-4a3d-8c3a-b369006a3fe3",
                EnCode = "MT002",
                FullName = "重金属"
            });
            list.Add(new MaterialType
            {
                Id = "a29339d4-5aca-41f2-af6c-06654e85c9e5",
                EnCode = "MT003",
                FullName = "金盐"
            });
            list.Add(new MaterialType
            {
                Id = "8e10aab6-2e4a-40f5-8d06-2282ede7296d",
                EnCode = "MT004",
                FullName = "化学药品"
            });
            return list;


        }

    }
}
