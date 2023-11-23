using System;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Application.UseCases.CreateAccount
{

    public class CreateAccountResponseProfile : Profile
    {
        public CreateAccountResponseProfile()
        {
            CreateMap<Account, CreateAccountResponse>();

        }
    }
}

