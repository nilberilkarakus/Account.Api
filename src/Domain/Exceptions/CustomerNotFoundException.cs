using System;
namespace Domain.Exceptions
{
    public class CustomerNotFoundException : ApplicationException
    {

        public CustomerNotFoundException(string message) : base(message) { }
    }
}

