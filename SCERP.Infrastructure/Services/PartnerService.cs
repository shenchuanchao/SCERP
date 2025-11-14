
using Microsoft.EntityFrameworkCore;
using SCERP.Core.Interfaces;
using SCERP.Domain;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;
using SCERP.Infrastructure.Data;

namespace SCERP.Infrastructure.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly AppDbContext _context;

        public PartnerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Partner> CreatePartnerAsync(CreatePartnerDto dto, string currentUser)
        {
            // 检查编码是否重复
            if (await _context.Partners.AnyAsync(p => p.EnCode == dto.EnCode))
            {
                throw new ArgumentException("合作伙伴编码已存在");
            }

            var partner = new Partner
            {
                EnCode = dto.EnCode,
                FullName = dto.FullName,
                ShortName = dto.ShortName,
                Description = dto.Description,
                Address = dto.Address,
                TelePhone = dto.TelePhone,
                Fax = dto.Fax,
                ContactName = dto.ContactName,
                Email = dto.Email,
                WebUrl = dto.WebUrl,
                BankName = dto.BankName,
                BankAccount = dto.BankAccount,
                TaxNumber = dto.TaxNumber,
                TaxRate = dto.TaxRate,
                IsPlainInvoice = dto.IsPlainInvoice,
                SettlementCurrency = dto.SettlementCurrency,
                SettlementMethod = dto.SettlementMethod,
                DeliveryMethod = dto.DeliveryMethod,
                DeliveryAddress = dto.DeliveryAddress,
                Category = dto.Category,
                Level = dto.Level,
                PartnerCategory = dto.PartnerCategory,
                CreatedUser = currentUser,
                CreatedTime = DateTime.Now,
                Status = PartnerStatus.Active
            };

            _context.Partners.Add(partner);
            await _context.SaveChangesAsync();

            return partner;
        }

        public async Task<Partner> GetPartnerByIdAsync(int id)
        {
            return await _context.Partners
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Partner>> GetPartnersAsync(PartnerCategory? category = null)
        {
            var query = _context.Partners.Where(p => !p.IsDeleted);

            if (category.HasValue)
            {
                query = query.Where(p => p.PartnerCategory == category.Value);
            }

            return await query.OrderByDescending(p => p.CreatedTime).ToListAsync();
        }

        public async Task<Partner> UpdatePartnerAsync(int id, CreatePartnerDto dto, string currentUser)
        {
            var partner = await _context.Partners
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
            {
                throw new ArgumentException("合作伙伴不存在");
            }

            // 检查编码是否重复（排除自己）
            if (await _context.Partners.AnyAsync(p => p.EnCode == dto.EnCode && p.Id != id))
            {
                throw new ArgumentException("合作伙伴编码已存在");
            }

            partner.EnCode = dto.EnCode;
            partner.FullName = dto.FullName;
            partner.ShortName = dto.ShortName;
            partner.Description = dto.Description;
            partner.Address = dto.Address;
            partner.TelePhone = dto.TelePhone;
            partner.Fax = dto.Fax;
            partner.ContactName = dto.ContactName;
            partner.Email = dto.Email;
            partner.WebUrl = dto.WebUrl;
            partner.BankName = dto.BankName;
            partner.BankAccount = dto.BankAccount;
            partner.TaxNumber = dto.TaxNumber;
            partner.TaxRate = dto.TaxRate;
            partner.IsPlainInvoice = dto.IsPlainInvoice;
            partner.SettlementCurrency = dto.SettlementCurrency;
            partner.SettlementMethod = dto.SettlementMethod;
            partner.DeliveryMethod = dto.DeliveryMethod;
            partner.DeliveryAddress = dto.DeliveryAddress;
            partner.Category = dto.Category;
            partner.Level = dto.Level;
            partner.PartnerCategory = dto.PartnerCategory;
            partner.ModifiedUser = currentUser;
            partner.ModifiedTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return partner;
        }

        public async Task<bool> DeletePartnerAsync(int id, string currentUser)
        {
            var partner = await _context.Partners
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
            {
                return false;
            }

            partner.IsDeleted = true;
            partner.DeletedUser = currentUser;
            partner.DeletedTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
