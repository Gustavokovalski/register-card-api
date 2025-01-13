using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegisterCard.Application.Common.Exceptions;
using RegisterCard.Application.Common.Models;
using RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;
using RegisterCard.Application.UseCases.Cards.Queries.GetUserById;
using RegisterCard.WebApi.Utils;

namespace RegisterCard.WebApi.Controllers;

[Route("cards")]
[ServiceFilter(typeof(ApiResponseFilter))]
public class CardController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardController(IMediator mediator)
    {
        _mediator = mediator.ThrowIfNull();
    }

    /// <summary>
    /// Register a card
    /// </summary>
    /// <remarks>
    /// registers a card token in the database.
    /// </remarks>
    [HttpPost("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<RegisterCardResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<object>))]
    public async Task<IActionResult> Register(
        [FromHeader(Name = "CustomerId"), Required] int customerId,
        [FromBody] RegisterCardCommand command,
        CancellationToken cancellationToken)
    {
        command.CustomerId = customerId;
        var created = await _mediator.Send(command, cancellationToken);
        return Ok(created);
    }

    /// <summary>
    /// List all cards
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<GetAllResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponse<object>))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var cards = await _mediator.Send(new GetAllQuery(), cancellationToken);
        return Ok(cards);
    }
}