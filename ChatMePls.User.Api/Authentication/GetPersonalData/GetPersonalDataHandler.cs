using System.Security.Claims;
using ChatMePls.DefaultServices.CQRS;
using ChatMePls.User.Api.Application;
using ChatMePls.User.Api.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ChatMePls.User.Api.Authentication.GetPersonalData;

public record GetPersonalDataRequest(string UserUid) : IQuery<GetPersonalDataResult>;

public class GetPersonalDataResult
{
    public Guid Uid { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Gender { get; init; }
    public string DateOfBirth { get; init; }
}

public class GetPersonalDataHandler(UserManager<ApplicationUser> userManager)
    : IQueryHandler<GetPersonalDataRequest, GetPersonalDataResult>
{
    public async Task<GetPersonalDataResult> Handle(GetPersonalDataRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserUid);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserUid);
        }
        
        var claims = await userManager.GetClaimsAsync(user);
        var model = new GetPersonalDataResult
        {
            Uid = user.Id,
            Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
            Name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
            Surname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
            Gender = claims.FirstOrDefault(x => x.Type == ClaimTypes.Gender)?.Value,
            DateOfBirth = claims.FirstOrDefault(x => x.Type == ClaimTypes.DateOfBirth)?.Value,
        };

        return model;
    }
}