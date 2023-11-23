using System;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.GetAccountTransactions
{
    public class GetAccountTransactionsResponseProfile : Profile
    {
        public GetAccountTransactionsResponseProfile()
        {
            CreateMap<Transaction, GetAccountTransactionsResponse>();

        }
    }
}

