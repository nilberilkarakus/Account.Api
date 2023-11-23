using System;
namespace Domain.Exceptions
{

    public class TransactionNotFoundException : ApplicationException
    {

        public TransactionNotFoundException(string message) : base(message) { }
    }
}

