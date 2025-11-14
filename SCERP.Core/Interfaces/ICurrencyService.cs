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
        Task<Currency> CreateCurrencyAsync(RequestCurrencyDto dto);
        Task<Currency> GetCurrencyByIdAsync(string id);
        Task<List<Currency>> GetCurrencysAsync();
        Task<Currency> UpdateCurrencyAsync(int id, RequestCurrencyDto dto);
        Task<bool> DeleteCurrencyAsync(int id);
    }
}
