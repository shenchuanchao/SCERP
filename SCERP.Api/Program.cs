using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SCERP.Identity;
using SCERP.Infrastructure;
using SCERP.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 添加基础设施层服务（包括数据库上下文和业务服务）
builder.Services.AddInfrastructure(builder.Configuration);

// 添加控制器和API探索
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddOpenApi();

// 添加Swagger服务
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    #region 配置XML注释路径
    string xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "SCERP.Api.xml");
    c.IncludeXmlComments(xmlPath);
    #endregion

    #region 配置Swagger文档信息(接口分组或版本)
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SCERP API",
        Version = "v1",
        Description = "SCERP API Description"
    });
    #endregion

    // 添加JWT认证到Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 配置CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SCERPAPP", policy =>
    {
        policy.WithOrigins("https://localhost:7122", "http://localhost:5122") // 前端应用的地址
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", "SCERP API V1");
        c.RoutePrefix = "swagger"; // 在/swagger访问Swagger UI
    });
#if DEBUG
    // 开发环境下自动应用数据库迁移
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("正在检查数据库...");

            // 确保数据库已创建
            if (!dbContext.Database.CanConnect())
            {
                logger.LogInformation("数据库不存在，正在创建...");
                await dbContext.Database.EnsureCreatedAsync();
                logger.LogInformation("数据库创建完成");
            }
            else
            {
                logger.LogInformation("数据库已存在，检查表结构...");

                // 检查是否有表
                var tables = dbContext.Model.GetEntityTypes();
                logger.LogInformation("发现的实体类型: {Count}", tables.Count());

                foreach (var entityType in tables)
                {
                    logger.LogInformation("实体: {Entity}", entityType.Name);
                }

                // 应用迁移
                logger.LogInformation("正在应用迁移...");
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("迁移应用完成");
            }

            // 初始化角色和用户
            logger.LogInformation("正在初始化角色和用户...");
            await InitializeRolesAndUsers(scope.ServiceProvider);
            logger.LogInformation("角色和用户初始化完成");
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "数据库初始化过程中发生错误");
        }
    }
#endif
}
app.UseHttpsRedirection();
// 启用CORS（必须在UseAuthentication和UseAuthorization之前）
app.UseCors("SCERPAPP");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#region 初始化角色和用户的辅助方法
async Task InitializeRolesAndUsers(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // 创建角色
    string[] roleNames = { "Admin", "Customer" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // 创建默认管理员用户
    var adminUser = await userManager.FindByEmailAsync("admin@scerp.com");
    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@scerp.com",
            FullName = "系统管理员",
            EmailConfirmed = true
        };

        var createPowerUser = await userManager.CreateAsync(user, "Admin123!");
        if (createPowerUser.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
#endregion