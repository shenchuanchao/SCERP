using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.Models
{
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
        [MaxLength(10)]
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string? FullName { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        [Required]
        public decimal ExchangeRate { get; set; } = 1;

    }
}
