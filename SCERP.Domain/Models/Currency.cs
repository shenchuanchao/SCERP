using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.Models
{
    /// <summary>
    /// 货币实体
    /// </summary>
    public class Currency
    {
        public Currency() { }
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        /// <summary>
        /// 币名代码
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string EnCode { get; set; }
        /// <summary>
        /// 币名符号
        /// </summary>
        [MaxLength(3)]
        public string? Symbol { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        [Required]
        public decimal ExchangeRate { get; set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewedAt { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [MaxLength(50)]
        public string? ReviewedBy { get; set; }


    }

    /// <summary>
    /// 货币操作日志实体
    /// </summary>
    public class CurrencyLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 货币Id
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CurrencyId { get; set; }
        /// <summary>
        /// 操作日志
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
