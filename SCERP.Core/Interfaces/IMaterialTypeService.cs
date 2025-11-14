using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;

namespace SCERP.Core.Interfaces
{
    /// <summary>
    /// 物料类型服务接口
    /// </summary>
    public interface IMaterialTypeService
    {
        /// <summary>
        /// 查询物料类型列表
        /// </summary>
        /// <returns></returns>
        Task<List<MaterialType>> GetMaterialTypesAsync();
        /// <summary>
        /// 根据ID获取物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MaterialType> GetMaterialTypeAsync(string id);
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        Task AddMaterialTypeAsync(MaterialRequest materialType);
        /// <summary>
        /// 更新物料类型
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        Task UpdateMaterialTypeAsync(string Id, MaterialRequest materialType);
        /// <summary>
        /// 删除物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteMaterialTypeAsync(string id);



    }
}
