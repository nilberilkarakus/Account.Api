using System;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class Extensions
{

    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddRepositories();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepository,AccountRepository>();
        services.AddSingleton<ICustomerRepository,CustomerRepositiory>();
        services.AddSingleton<ITransactionRepository,TransactionRepository>();
    }

}

