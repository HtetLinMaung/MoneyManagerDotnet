using AutoMapper;
using MoneyManager.DTOs;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Utilities;

namespace MoneyManager.Services
{
    public class FinancialTransactionService : IFinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _financialTransactionRepository;
        private readonly IMapper _mapper;

        public FinancialTransactionService(IFinancialTransactionRepository financialTransactionRepository, IMapper mapper)
        {
            _financialTransactionRepository = financialTransactionRepository;
            _mapper = mapper;
        }

        public Task<FinancialTransaction> AddTransactionAsync(CreateFinancialTransactionDto createFinancialTransactionDto)
        {
            var financialTransaction = _mapper.Map<FinancialTransaction>(createFinancialTransactionDto);
            return _financialTransactionRepository.AddAsync(financialTransaction);
        }

        public Task DeleteTransactionAsync(int id, string userId)
        {
            return _financialTransactionRepository.DeleteAsync(id, userId);
        }

        public Task<FinancialTransactionsResponse> GetAllTransactionsAsync(int page, int limit, string fromDate, string toDate, string userId)
        {
            return _financialTransactionRepository.GetAllAsync(page, limit, fromDate, toDate, userId);
        }

        public Task<FinancialTransaction?> GetTransactionByIdAsync(int id, string userId)
        {
            return _financialTransactionRepository.GetByIdAsync(id, userId);
        }

        public Task UpdateTransactionAsync(UpdateFinancialTransactionDto updateFinancialTransactionDto)
        {
            var financialTransaction = _mapper.Map<FinancialTransaction>(updateFinancialTransactionDto);
            return _financialTransactionRepository.UpdateAsync(financialTransaction);
        }
    }
}