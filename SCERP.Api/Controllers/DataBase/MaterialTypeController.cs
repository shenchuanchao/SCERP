using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCERP.Core.Interfaces;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;

namespace SCERP.Api.Controllers.DataBase
{
    /// <summary>
    /// 物料类型控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialTypeController : ControllerBase
    {
        private readonly IMaterialTypeService _materialTypeService;
        private readonly ILogger<MaterialTypeController> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="materialTypeService"></param>
        /// <param name="logger"></param>
        public MaterialTypeController(IMaterialTypeService materialTypeService, ILogger<MaterialTypeController> logger)
        {
            _materialTypeService = materialTypeService;
            _logger = logger;
        }

        /// <summary>
        /// 查询物料类型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaterialTypes()
        {
            var materialTypes = await _materialTypeService.GetMaterialTypesAsync();
            return Ok(materialTypes);
        }

        /// <summary>
        /// 根据ID获取物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialType(string id)
        {
            var materialType = await _materialTypeService.GetMaterialTypeAsync(id);
            if (materialType == null)
            {
                return NotFound();
            }
            return Ok(materialType);
        }
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMaterialType([FromBody] MaterialRequest materialType)
        {
            await _materialTypeService.AddMaterialTypeAsync(materialType);
            return Ok(ApiResponse.Success("物料类型添加成功"));
        }
        /// <summary>
        /// 更新物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="materialType"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterialType(string id, [FromBody] MaterialRequest materialType)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            await _materialTypeService.UpdateMaterialTypeAsync(id,materialType);
            return Ok(ApiResponse.Success("物料类型更新成功"));
        }
        /// <summary>
        /// 删除物料类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialType(string id)
        {
            await _materialTypeService.DeleteMaterialTypeAsync(id);
            return NoContent();
        }

    }
}
