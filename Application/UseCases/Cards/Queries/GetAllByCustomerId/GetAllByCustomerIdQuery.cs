using MediatR;

namespace RegisterCard.Application.UseCases.Cards.Queries.GetUserById;

public record class GetAllByCustomerIdQuery(int CustomerId) : IRequest<IEnumerable<GetAllByCustomerIdResponse>>
{
}