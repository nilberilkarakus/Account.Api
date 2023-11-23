using System;
using System.Reflection;
using Application.PipelineBehaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.UseCases.CreateAccount;
using Application.UseCases.GetCustomer;

namespace Application
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddPipelineBehavior();
            services.AddValidations();
        }

        public static void AddPipelineBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        public static void AddValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
            services.AddValidatorsFromAssemblyContaining<GetCustomerValidator>();

        }
    }
}