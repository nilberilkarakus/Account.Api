using System;
using System.Numerics;
using Application.UseCases.GetAccountTransactions;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Application.UseCases.GetCustomer
{

    public class GetCustomerResponseProfile : Profile
    {
        public GetCustomerResponseProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<Account, AccountDTO>();
        }
    }
}

