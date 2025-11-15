using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain.Models;

namespace SCERP.Domain.DTOs
{
    public class CreatePartnerDto
    {
        [Required(ErrorMessage = "编码不能为空")]
        [MaxLength(50, ErrorMessage = "编码长度不能超过50个字符")]
        public string EnCode { get; set; }

        [Required(ErrorMessage = "全称不能为空")]
        [MaxLength(200, ErrorMessage = "全称长度不能超过200个字符")]
        public string FullName { get; set; }

        [MaxLength(100, ErrorMessage = "简称长度不能超过100个字符")]
        public string? ShortName { get; set; }

        [MaxLength(500, ErrorMessage = "描述长度不能超过500个字符")]
        public string? Description { get; set; }

        [MaxLength(300, ErrorMessage = "地址长度不能超过300个字符")]
        public string? Address { get; set; }

        [MaxLength(50, ErrorMessage = "电话长度不能超过50个字符")]
        public string? TelePhone { get; set; }

        [MaxLength(20, ErrorMessage = "传真长度不能超过20个字符")]
        public string? Fax { get; set; }

        [MaxLength(50, ErrorMessage = "联系人长度不能超过50个字符")]
        public string? ContactName { get; set; }

        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [MaxLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        public string? Email { get; set; }

        [Url(ErrorMessage = "网址格式不正确")]
        [MaxLength(200, ErrorMessage = "网址长度不能超过200个字符")]
        public string? WebUrl { get; set; }

        [MaxLength(100, ErrorMessage = "开户行长度不能超过100个字符")]
        public string? BankName { get; set; }

        [MaxLength(50, ErrorMessage = "账号长度不能超过50个字符")]
        public string? BankAccount { get; set; }

        [MaxLength(50, ErrorMessage = "税号长度不能超过50个字符")]
        public string? TaxNumber { get; set; }

        [Range(0, 1, ErrorMessage = "税率必须在0到1之间")]
        public decimal TaxRate { get; set; } = 0.13m;

        public bool IsPlainInvoice { get; set; } = false;

        [MaxLength(20, ErrorMessage = "结算币名长度不能超过20个字符")]
        public string SettlementCurrency { get; set; } = "RMB";

        public SettlementMethods SettlementMethod { get; set; }= SettlementMethods.Cash;

        [MaxLength(50, ErrorMessage = "交货方式长度不能超过50个字符")]
        public string? DeliveryMethod { get; set; }

        [MaxLength(300, ErrorMessage = "交货地址长度不能超过300个字符")]
        public string? DeliveryAddress { get; set; }

        [MaxLength(50, ErrorMessage = "类别长度不能超过50个字符")]
        public string? Category { get; set; }

        [MaxLength(20, ErrorMessage = "级别长度不能超过20个字符")]
        public string Level { get; set; } = "普通";

        public PartnerCategory PartnerCategory { get; set; } = PartnerCategory.Customer;
    }

    public class PartnerDto
    {
        public int Id { get; set; }
        public string EnCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? TelePhone { get; set; }
        public string? Fax { get; set; }
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? WebUrl { get; set; }
        public string? BankName { get; set; }
        public string? BankAccount { get; set; }
        public string? TaxNumber { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsPlainInvoice { get; set; }
        public string SettlementCurrency { get; set; } = "RMB";
        public SettlementMethods SettlementMethod { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Category { get; set; }
        public string Level { get; set; } = "普通";
        public PartnerStatus Status { get; set; }
        public PartnerCategory PartnerCategory { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
    }
}
