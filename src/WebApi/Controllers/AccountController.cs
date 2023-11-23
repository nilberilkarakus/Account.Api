using Application.UseCases.CreateAccount;
using MediatR;
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest createAccountRequest)
    {
        return await _mediator.Send(createAccountRequest);
    }

}

