using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class CustomerRepositiory : ICustomerRepository
    {
        public CustomerRepositiory()
        {
            using (var context = new AccountApiDbContext())
            {
                var customers = new List<Customer>
                {
                new Customer
                {
                    Id = new Guid("93527517-56ee-4e7f-9777-794fb193138d"),
                    Name = "TestName1",
                    Surname = "TestSurname1",
                    AccountId = new Guid("02fffc63-603c-40d6-bc64-451652cde192")
                },
                new Customer
                {
                    Id = new Guid("0d031dc6-e26e-4ed7-8424-7c6314695150"),
                    Name = "TestName2",
                    Surname = "TestSurname2",
                    AccountId = new Guid("e976f964-aaa1-4e22-8f8c-d75bd355643a")
                },
             };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }
        public async Task<Customer> GetCustomerById(Guid CustomerId)
        {
            using (var context = new AccountApiDbContext())
            {
                var customer = await context.Customers.Where(customer => customer.Id == CustomerId).FirstOrDefaultAsync();

                return customer;
            }
        }
    }
}

