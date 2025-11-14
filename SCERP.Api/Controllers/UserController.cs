using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SCERP.Domain.DTOs;
using SCERP.Identity;

namespace SCERP.Api.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
        /// <summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("用户创建成功: {Email}", model.Email);
                return Ok(new { code = 0, message = "用户创建成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("用户更新成功: {Email}", user.Email);
                return Ok(new { message = "用户更新成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("用户删除成功: {Email}", user.Email);
                return Ok(new { message = "用户删除成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        /// <summary>
        /// 获取用户角色的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        /// <summary>
        /// 分配角色给用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost("{id}/roles")]
        public async Task<IActionResult> AssignRolesToUser(string id, [FromBody] List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //判断roles的合法性
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    //这里可以添加对角色合法性的检查，比如查询数据库中的角色列表
                    if (!IsValidRole(role))
                    {
                        return BadRequest(new { message = $"角色 {role} 不存在" });
                    }
                }
            }
            // 获取用户当前角色
            var currentRoles = await _userManager.GetRolesAsync(user);
            // 移除现有角色
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest(removeResult.Errors);
            }
            // 分配新角色
            var result = await _userManager.AddToRolesAsync(user, roles);
            if (result.Succeeded)
            {
                _logger.LogInformation("角色分配成功给用户: {Email}", user.Email);
                return Ok(new { message = "角色分配成功" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        /// <summary>
        /// 验证角色是否合法
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool IsValidRole(string role)
        {
            // 获取系统中所有角色的列表进行验证
            var allRoles = _roleManager.Roles.Select(a=>a.Name).ToList();
            return allRoles.Contains(role);
        }
    }
}
