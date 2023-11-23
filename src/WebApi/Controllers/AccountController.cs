using Application.UseCases.CreateAccount;
using Application.UseCases.GetAccountTransactions;
using Application.UseCases.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;


    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("accounts")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest createAccountRequest)
    {
        var createdAccount = await _mediator.Send(createAccountRequest);

        return Created(new Uri(Request.GetEncodedUrl() + "/" + createdAccount.Id), createdAccount);

    }

    [HttpGet("accounts/transactions/{AccountId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<GetAccountTransactionsResponse>> GetAccountTransactions([FromRoute] GetAccountTransactionsRequest getAccountTransactionsRequest)
    { 
        return await _mediator.Send(getAccountTransactionsRequest);

    }

}

