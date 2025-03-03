using System.ComponentModel.DataAnnotations;
using ChatMePls.Profile.Client.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.Gateway.Profile;

public record CreateUserRequestModel(
    [Required, DataType(DataType.EmailAddress)] string Email,
    [Required] string Username,
    [Required, DataType(DataType.Password)] string Password);

public record CreateUserStatus(bool Status);

public record GetTokenRequestoModel(
    [Required] string Username,
    [Required, DataType(DataType.Password)] string Password);

[ApiController]
[Route("[controller]")]
public class ProfileEndpoints(ISender sender) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        var command = new CreateUserCommand(request.Email, request.Username, request.Password);
        var response = await sender.Send(command);
        
        return Ok(new CreateUserStatus(response.Success));
    }

    // [HttpPost]
    // [AllowAnonymous]
    // public async Task<IActionResult> GetToken(GetTokenRequestoModel request)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return ValidationProblem();
    //     }
    //
    //     return Ok();
    // }
}