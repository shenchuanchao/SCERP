using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;
using SCERP.Domain;

namespace SCERP.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetCurrencysAsync();
        Task<Currency> GetCurrencyByIdAsync(string id);
        Task CreateCurrencyAsync(RequestCurrencyDto dto);
        Task<bool> UpdateCurrencyAsync(string id, RequestCurrencyDto dto);
        Task<bool> DeleteCurrencyAsync(string id);

        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="currencyId"></param>
        /// <param name="operation"></param>
        /// <param name="performedBy"></param>
        /// <returns></returns>
        Task LogCurrencyAsync(string currencyId, string operation, string performedBy);
        /// <summary>
        /// 获取日志记录
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task<List<CurrencyLog>> GetCurrencyLogsAsync(string currencyId);

    }
}
