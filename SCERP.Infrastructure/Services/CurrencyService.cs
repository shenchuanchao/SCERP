using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SCERP.Core.Interfaces;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;
using SCERP.Infrastructure.Data;

namespace SCERP.Infrastructure.Services
{
    /// <summary>
    /// 货币服务接口实现
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(AppDbContext context, ILogger<CurrencyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 获取货币列表
        /// </summary>
        /// <returns></returns>
        public Task<List<Currency>> GetCurrencysAsync()
        {
            return Task.FromResult(_context.Currencies.ToList());
        }
        /// <summary>
        /// 根据Id获取货币
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<Currency> GetCurrencyByIdAsync(string Id)
        {
            var currency = _context.Currencies.FirstOrDefault(mt => mt.Id == Id);
            return Task.FromResult(currency);

        }

        /// <summary>
        /// 创建货币
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateCurrencyAsync(RequestCurrencyDto dto)
        {
            try
            {
                var newCurrency = new Currency
                {
                    Id = Guid.NewGuid().ToString(),
                    EnCode = dto.EnCode,
                    Symbol= dto.Symbol,
                    ExchangeRate = dto.ExchangeRate,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Admin"
                };
                //添加日志
                await LogCurrencyAsync(newCurrency.Id, "创建", "Admin");
                _context.Currencies.Add(newCurrency);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建货币失败");
                throw;
            }

        }

        /// <summary>
        /// 修改货币
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<bool> UpdateCurrencyAsync(string Id, RequestCurrencyDto dto)
        {
            var currency = _context.Currencies.FirstOrDefault(mt => mt.Id == Id);
            if (currency != null)
            {
                currency.EnCode = dto.EnCode;
                currency.Symbol=dto.Symbol;
                currency.ExchangeRate = dto.ExchangeRate;
                //添加日志
                LogCurrencyAsync(Id, "修改", "Admin");
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// 删除货币
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<bool> DeleteCurrencyAsync(string Id)
        {
            var currency = _context.Currencies.FirstOrDefault(mt => mt.Id == Id);
            if (currency != null)
            {
                _context.Currencies.Remove(currency);
                _context.SaveChanges();
            }
            return Task.FromResult(true);
        }


        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="currencyId"></param>
        /// <param name="operation"></param>
        /// <param name="performedBy"></param>
        /// <returns></returns>
        public Task LogCurrencyAsync(string currencyId, string operation, string performedBy)
        {
            var log = new CurrencyLog
            {
                CurrencyId = currencyId,
                Description = $"{operation}:{performedBy} {DateTime.Now}",
            };
            _context.CurrencyLogs.Add(log);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 查询日志记录
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        public Task<List<CurrencyLog>> GetCurrencyLogsAsync(string currencyId)
        {
            return Task.FromResult(_context.CurrencyLogs.Where(l => l.CurrencyId == currencyId).ToList());
        }

    }
}
