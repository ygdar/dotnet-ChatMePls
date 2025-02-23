using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatMePls.DefaultServices.CQRS;
using ChatMePls.User.Api.Application;
using ChatMePls.User.Api.JwtKey;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.Authentication.SignInUser;

public record SignInUserCommand([Required] string UserName, [Required] string Password)
    : ICommand<SignInUserResult>;

public record SignInUserResult(string Token);

public class SignInUserHandler(
    JwtSecurityTokenHandler jwtTokenHandler,
    IJwtSigningEncodingKey signingEncodingKey,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager
)
    : ICommandHandler<SignInUserCommand, SignInUserResult>
{
    public async Task<SignInUserResult> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        
        var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, false, false);

        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
        };
        
        var token = jwtTokenHandler.CreateJwtSecurityToken(
            issuer: GetType().Assembly.GetName().Name,
            audience: typeof(SignInUserHandler).Assembly.GetName().Name,
            subject: new ClaimsIdentity(claims),
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: new SigningCredentials(
                signingEncodingKey.GetKey(),
                signingEncodingKey.SigningAlgorithm)
        );
        
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new SignInUserResult(jwtToken);
    }
}