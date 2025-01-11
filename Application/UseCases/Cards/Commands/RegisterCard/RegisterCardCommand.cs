using System.Text.Json.Serialization;
using MediatR;

namespace RegisterCard.Application.UseCases.Cards.Commands.RegisterCard;

//public class CreateUserCommand : IRequest<CreateUserResponse>
//{
public class RegisterCardCommand : IRequest<RegisterCardResponse>
{
    [JsonIgnore]
    public int CustomerId { get; set; }

    [JsonRequired]
    public string? CardNumber { get; set; }

    [JsonRequired]
    public string? Cvv { get; set; }
    public string? Provider { get; set; }
}

//    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
//    {
//        private readonly IUserRepository _repository;

//        public CreateUserCommandHandler(IUserRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
//        {
//            var user = new User { Name = request.Name, Email = request.Email, Profile = new UserProfile { Name = "Admin" } };
//            await _repository.Insert(user, cancellationToken);
//            return new CreateUserResponse(user.Id.ToString());
//        }
//    }
//}