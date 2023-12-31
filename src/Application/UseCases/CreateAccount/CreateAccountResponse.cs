﻿using System;

namespace Application.UseCases.CreateAccount
{
	public class CreateAccountResponse
	{
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CustomerId { get; set; }

        public long Balance { get; set; }
    }
}

