using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain.Models;

namespace SCERP.Domain.SeedData
{
    /// <summary>
    /// 货币种子数据
    /// </summary>
    public class CurrencyList
    {
        /// <summary>
        /// 数据
        /// </summary>
        /// <returns></returns>
        public static List<Currency> Data()
        {
            return new List<Currency>
            {
                new Currency
                {
                    Id = "6b9066b7-feda-4b46-aeba-b36adcf493f3",
                    EnCode = "HKD",
                    Symbol = "HK$",
                    ExchangeRate = 0.8m,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                },
                new Currency
                {
                    Id = "8b293536-2f39-4800-abdb-e41e0d38bb87",
                    EnCode = "RMB",
                    Symbol = "￥",
                    ExchangeRate = 1.0m,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "system",
                },
            };

        }
    }
}
