using Microsoft.AspNetCore.Mvc;
using SCERP.Core.Interfaces;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;
using SCERP.Domain;

namespace SCERP.Api.Controllers
{
    /// <summary>
    /// 供应商控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public SupplierController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpPost]
        public async Task<ApiResponse<PartnerDto>> CreatePartner([FromBody] CreatePartnerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse<PartnerDto>.Failure("数据验证失败", 400);
            }

            try
            {
                // 获取当前用户（实际项目中可能从JWT token中获取）
                var currentUser = User.Identity?.Name ?? "System";

                var partner = await _partnerService.CreatePartnerAsync(dto, currentUser);
                var result = MapToDto(partner);

                return ApiResponse<PartnerDto>.Success(result);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse<PartnerDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                // 记录日志
                return ApiResponse<PartnerDto>.Failure("创建供应商失败", 500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PartnerDto>> GetPartner(int id)
        {
            var partner = await _partnerService.GetPartnerByIdAsync(id);

            if (partner == null)
            {
                return ApiResponse<PartnerDto>.Failure("供应商不存在", 404);
            }

            var result = MapToDto(partner);
            return ApiResponse<PartnerDto>.Success(result);
        }

        [HttpGet]
        public async Task<ApiResponse<List<PartnerDto>>> GetPartners([FromQuery] PartnerCategory? category = null)
        {
            var partners = await _partnerService.GetPartnersAsync(category);
            var result = partners.Select(MapToDto).ToList();

            return ApiResponse<List<PartnerDto>>.Success(result);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<PartnerDto>> UpdatePartner(int id, [FromBody] CreatePartnerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse<PartnerDto>.Failure("数据验证失败", 400);
            }

            try
            {
                var currentUser = User.Identity?.Name ?? "System";
                var partner = await _partnerService.UpdatePartnerAsync(id, dto, currentUser);
                var result = MapToDto(partner);

                return ApiResponse<PartnerDto>.Success(result);
            }
            catch (ArgumentException ex)
            {
                return ApiResponse<PartnerDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ApiResponse<PartnerDto>.Failure("更新合作伙伴失败", 500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeletePartner(int id)
        {
            var currentUser = User.Identity?.Name ?? "System";
            var result = await _partnerService.DeletePartnerAsync(id, currentUser);

            if (!result)
            {
                return ApiResponse<bool>.Failure("合作伙伴不存在", 404);
            }

            return ApiResponse<bool>.Success(true);
        }

        private PartnerDto MapToDto(Partner partner)
        {
            return new PartnerDto
            {
                Id = partner.Id,
                EnCode = partner.EnCode,
                FullName = partner.FullName,
                ShortName = partner.ShortName,
                Description = partner.Description,
                Address = partner.Address,
                TelePhone = partner.TelePhone,
                Fax = partner.Fax,
                ContactName = partner.ContactName,
                Email = partner.Email,
                WebUrl = partner.WebUrl,
                BankName = partner.BankName,
                BankAccount = partner.BankAccount,
                TaxNumber = partner.TaxNumber,
                TaxRate = partner.TaxRate,
                IsPlainInvoice = partner.IsPlainInvoice,
                SettlementCurrency = partner.SettlementCurrency,
                SettlementMethod = partner.SettlementMethod,
                DeliveryMethod = partner.DeliveryMethod,
                DeliveryAddress = partner.DeliveryAddress,
                Category = partner.Category,
                Level = partner.Level,
                Status = partner.Status,
                PartnerCategory = partner.PartnerCategory,
                CreatedTime = partner.CreatedTime,
                CreatedUser = partner.CreatedUser
            };
        }


    }
}
