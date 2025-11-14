using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCERP.Identity;

namespace SCERP.Api.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthController> _logger;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
        /// <summary>
        /// 根据ID查询角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        /// <summary>
        /// 创建新角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                return BadRequest(new { message = "角色已存在" });
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                _logger.LogInformation("角色创建成功: {RoleName}", roleName);
                return Ok(new { message = "角色创建成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleName"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                _logger.LogInformation("角色更新成功: {RoleName}", roleName);
                return Ok(new { message = "角色更新成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                _logger.LogInformation("角色删除成功: {RoleName}", role.Name);
                return Ok(new { message = "角色删除成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


    }
}
