using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.User.Api.Registration.CreateUser;

public record CreateUserRequest(
    [Required, DataType(DataType.EmailAddress)] string Email,
    [Required] string Username,
    [Required, DataType(DataType.Password)] string Password);

public record CreateUserResponse(bool Status);


[ApiController, AllowAnonymous]
[Route("[controller]")]
public class CreateUserEndpoint(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        var command = new CreateUserCommand(request.Email, request.Username, request.Password);
        var response = await sender.Send(command);
        
        return Ok(new CreateUserResponse(true));
    }
}