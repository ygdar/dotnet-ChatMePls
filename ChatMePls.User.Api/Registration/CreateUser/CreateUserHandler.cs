using ChatMePls.DefaultServices.CQRS;

namespace ChatMePls.User.Api.Registration.CreateUser;

public record CreateUserCommand(string Email, string Username, string Password)
    : ICommand<CreateUserResult>;

public record CreateUserResult(Guid UserUid);

public class CreateUserHandler : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}