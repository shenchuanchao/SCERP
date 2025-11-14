using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.DTOs
{
    public class RequestCurrencyDto
    {
        [Required(ErrorMessage = "编码不能为空")]
        [MaxLength(10,ErrorMessage = "编码长度不能超过10个字符")]
        public string EnCode { get; set; }
        public string? FullName { get; set; }
        public decimal ExchangeRate { get; set; } = 1;
    }
}
