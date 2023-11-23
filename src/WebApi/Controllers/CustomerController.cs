using Application.UseCases.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;


    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("customers/{CustomerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GetCustomerResponse> GetCustomer([FromRoute] GetCustomerRequest getCustomerRequest)
    {
        return await _mediator.Send(getCustomerRequest);

    }

}

