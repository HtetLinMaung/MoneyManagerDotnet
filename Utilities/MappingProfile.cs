using AutoMapper;
using MoneyManager.DTOs;
using MoneyManager.Models;

namespace MoneyManager.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateFinancialTransactionDto, FinancialTransaction>();
            CreateMap<UpdateFinancialTransactionDto, FinancialTransaction>();
        }
    }
}