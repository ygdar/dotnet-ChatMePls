using ChatMePls.DefaultServices.CQRS;
using ChatMePls.User.Api.Application;
using Microsoft.AspNetCore.Identity;

namespace ChatMePls.User.Api.Registration.CreateUser;

public record CreateUserCommand(string Email, string Username, string Password)
    : ICommand<CreateUserResult>;

public record CreateUserResult(Guid UserUid);

public class CreateUserHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var iu = new ApplicationUser()
        {
            UserName = request.Username,
            Email = request.Email,
        };
        
        var result = await userManager.CreateAsync(iu, request.Password);

        return new CreateUserResult(iu.Id);
    }
}