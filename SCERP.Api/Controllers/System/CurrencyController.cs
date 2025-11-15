using Microsoft.AspNetCore.Mvc;
using SCERP.Core.Interfaces;
using SCERP.Domain.DTOs;
using SCERP.Domain.Models;
using SCERP.Infrastructure.Services;

namespace SCERP.Api.Controllers.System
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currencyService"></param>
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        /// <summary>
        /// 获取货币列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = await _currencyService.GetCurrencysAsync();
            return Ok(ApiResponse<List<Currency>>.Success(currencies));
        }
        /// <summary>
        /// 根据Id获取货币
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyById(string id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
            {
                return NotFound(ApiResponse<Currency>.Failure("货币未找到", 404));
            }
            return Ok(ApiResponse<Currency>.Success(currency));
        }
        /// <summary>
        /// 创建货币
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] RequestCurrencyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Failure("无效的请求数据", 400));
            }
            await _currencyService.CreateCurrencyAsync(dto);
            return Ok(ApiResponse.Success("货币创建成功"));
        }
        /// <summary>
        /// 更新货币
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCurrency([FromQuery] string id, [FromBody] RequestCurrencyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Failure("无效的请求数据", 400));
            }
            var result = await _currencyService.UpdateCurrencyAsync(id, dto);
            if (!result)
            {
                return NotFound(ApiResponse.Failure("货币未找到", 404));
            }
            return Ok(ApiResponse.Success("货币更新成功"));
        }

        /// <summary>
        /// 删除货币
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(string id)
        {
            var result = await _currencyService.DeleteCurrencyAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse.Failure("货币未找到", 404));
            }
            return Ok(ApiResponse.Success("货币删除成功"));
        }

        /// <summary>
        /// 获取货币日志
        /// </summary>
        /// <param name="currencyid"></param>
        /// <returns></returns>
        [HttpGet("{currencyid}/logs")]
        public async Task<IActionResult> GetCurrencyLogs(string currencyid)
        {
            var logs = await _currencyService.GetCurrencyLogsAsync(currencyid);
            return Ok(ApiResponse<List<CurrencyLog>>.Success(logs));
        }

    }
}
