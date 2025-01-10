using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
using RegisterCard.Application.UseCases.Cards.Queries.GetUserById;

namespace RegisterCard.WebApi.Controllers.v1;

[ApiVersion("1.0")]
[Route("cards")]
public class CardController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <remarks>
    /// Creates a new user in the database.
    /// </remarks>
    [HttpPost("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterCardResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromHeader(Name = "CustomerId")] int customerId,
        [FromBody] RegisterCardCommand command,
        CancellationToken cancellationToken)
    {
        command.CustomerId = customerId;
        var created = await _mediator.Send(command, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var cards = await _mediator.Send(new GetAllQuery(), cancellationToken);
        return Ok(cards);
    }

}