using MediatR;

namespace RegisterCard.Application.UseCases.Cards.Queries.GetUserById;

public class GetAllQuery : IRequest<IEnumerable<GetAllResponse>> { }