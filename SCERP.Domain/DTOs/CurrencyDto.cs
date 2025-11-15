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
        [Required(ErrorMessage = "币名编码不能为空")]
        [MaxLength(3, ErrorMessage = "币名编码长度不能超过3个字符")]
        public string EnCode { get; set; }

        [MaxLength(3, ErrorMessage = "币名符号长度不能超过3个字符")]
        public string Symbol { get; set; }
        public decimal ExchangeRate { get; set; } = 1;
    }
}
