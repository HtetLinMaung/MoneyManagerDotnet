using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.DTOs;
using MoneyManager.Models;
using MoneyManager.Services;
using MoneyManager.Utilities;

namespace MoneyManager.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/transactions")]
    [ApiController]
    public class FinancialTransactionController : ControllerBase
    {
        private readonly IFinancialTransactionService _financialTransactionService;

        public FinancialTransactionController(IFinancialTransactionService financialTransactionService)
        {
            _financialTransactionService = financialTransactionService;
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<FinancialTransaction>>> Post(CreateFinancialTransactionDto createFinancialTransactionDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            createFinancialTransactionDto.UserId = userId;
            var financialTransaction = await _financialTransactionService.AddTransactionAsync(createFinancialTransactionDto);
            return CreatedAtAction(nameof(Get), new { id = financialTransaction.Id }, new DataResponse<FinancialTransaction>
            {
                Code = StatusCodes.Status201Created,
                Message = "Financial transaction created successfully.",
                Data = financialTransaction
            });
        }

        [HttpGet]
        public async Task<ActionResult<FinancialTransactionsResponse>> GetAll(int page = 1, int limit = 10, string fromDate = "", string toDate = "")
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            if (string.IsNullOrWhiteSpace(fromDate))
            {
                fromDate = DateTime.Today.ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrWhiteSpace(toDate))
            {
                toDate = DateTime.Today.ToString("yyyy-MM-dd");
            }

            var paginatedList = await _financialTransactionService.GetAllTransactionsAsync(page, limit, fromDate, toDate, userId);
            return Ok(paginatedList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<FinancialTransaction>>> Get(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            var financialTransaction = await _financialTransactionService.GetTransactionByIdAsync(id, userId);
            if (financialTransaction == null)
            {
                return NotFound(new BaseResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Financial transaction not found!"
                });
            }
            return Ok(new DataResponse<FinancialTransaction>
            {
                Data = financialTransaction
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateFinancialTransactionDto updateFinancialTransactionDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            updateFinancialTransactionDto.Id = id;
            updateFinancialTransactionDto.UserId = userId!;
            await _financialTransactionService.UpdateTransactionAsync(updateFinancialTransactionDto);
            return Ok(new BaseResponse
            {
                Message = "Financial transaction updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _financialTransactionService.DeleteTransactionAsync(id, userId!);
            return Ok(new BaseResponse
            {
                Message = "Financial transaction deleted successfully."
            });
        }
    }
}