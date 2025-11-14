using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SCERP.Core.Interfaces;
using SCERP.Domain.DTOs;
using SCERP.Infrastructure.Data;

namespace SCERP.Infrastructure.Services
{
    /// <summary>
    /// 物料类型接口服务实现
    /// </summary>
    public class MaterialTypeService: IMaterialTypeService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MaterialTypeService> _logger;

        public MaterialTypeService(AppDbContext context, ILogger<MaterialTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 查询物料类型列表
        /// </summary>
        /// <returns></returns>
        public Task<List<Domain.Models.MaterialType>> GetMaterialTypesAsync()
        {
            return Task.FromResult(_context.MaterialTypes.ToList());

        }

        /// <summary>
        /// 根据ID获取物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Domain.Models.MaterialType> GetMaterialTypeAsync(string id)
        {
            var materialType = _context.MaterialTypes.FirstOrDefault(mt => mt.Id == id);
            return Task.FromResult(materialType);
        }
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public Task AddMaterialTypeAsync(MaterialRequest materialType)
        {
            var newMaterialType = new Domain.Models.MaterialType
            {
                Id = Guid.NewGuid().ToString(),
                EnCode = materialType.EnCode,
                FullName = materialType.FullName,
                Description = materialType.Description
            };
            _context.MaterialTypes.Add(newMaterialType);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 更新物料类型
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public Task UpdateMaterialTypeAsync(string Id, MaterialRequest materialType)
        {
            var existingMaterialType = _context.MaterialTypes.FirstOrDefault(mt => mt.Id == Id);
            if (existingMaterialType != null)
            {
                existingMaterialType.EnCode = materialType.EnCode;
                existingMaterialType.FullName = materialType.FullName;
                existingMaterialType.Description = materialType.Description;
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 删除物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteMaterialTypeAsync(string id)
        {
            var materialType = _context.MaterialTypes.FirstOrDefault(mt => mt.Id == id);
            if (materialType != null)
            {
                _context.MaterialTypes.Remove(materialType);
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }



    }
}
