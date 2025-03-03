using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatMePls.User.Api.JwtKey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatMePls.User.Api.Authentication.SignInUser;

public record SignInUserRequest([Required] string UserName, [Required] string Password);

public record SignInUserResponse(string token);

[ApiController, AllowAnonymous]
[Route("[controller]")]
public class SignInUserEndpoint(ISender sender)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SignInUser(SignInUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }
        
        var command = new SignInUserCommand(request.UserName, request.Password);
        var result  = await sender.Send(command);
        
        return Ok(new SignInUserResponse(result.Token));
    }
}