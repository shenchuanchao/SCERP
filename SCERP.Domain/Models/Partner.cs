using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.Models
{
    /// <summary>
    /// 合作伙伴（客户、供应商）
    /// </summary>
    public class Partner
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string EnCode { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [MaxLength(100)]
        public string? ShortName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(300)]
        public string? Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(50)]
        public string? TelePhone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [MaxLength(20)]
        public string? Fax { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(50)]
        public string? ContactName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        [MaxLength(200)]
        [Url]
        public string? WebUrl { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [MaxLength(100)]
        public string? BankName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(50)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// 税号
        /// </summary>
        [MaxLength(50)]
        public string? TaxNumber { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        [Range(0, 1)]
        public decimal TaxRate { get; set; } = 0.13m; // 默认13%

        /// <summary>
        /// 是否普票
        /// </summary>
        public bool IsPlainInvoice { get; set; } = true;

        /// <summary>
        /// 结算币名
        /// </summary>
        [MaxLength(20)]
        public string SettlementCurrency { get; set; } = "CNY";

        /// <summary>
        /// 结算方式
        /// </summary>
        [MaxLength(50)]
        public string? SettlementMethod { get; set; }

        /// <summary>
        /// 交货方式
        /// </summary>
        [MaxLength(50)]
        public string? DeliveryMethod { get; set; }

        /// <summary>
        /// 交货地址
        /// </summary>
        [MaxLength(300)]
        public string? DeliveryAddress { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [MaxLength(50)]
        public string? Category { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [MaxLength(20)]
        public string? Level { get; set; } = "普通";

        /// <summary>
        /// 状态
        /// </summary>
        public PartnerStatus Status { get; set; } = PartnerStatus.Active;

        /// <summary>
        /// 合作伙伴类别（客户、供应商）
        /// </summary>
        [Required]
        public PartnerCategory PartnerCategory { get; set; } = PartnerCategory.Customer;

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 添加用户
        /// </summary>
        [MaxLength(50)]
        public string CreatedUser { get; set; } = string.Empty;

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        [MaxLength(50)]
        public string? ModifiedUser { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewedTime { get; set; }

        /// <summary>
        /// 审核用户
        /// </summary>
        [MaxLength(50)]
        public string? ReviewedUser { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary>
        [MaxLength(50)]
        public string? DeletedUser { get; set; }
    }

   
}
