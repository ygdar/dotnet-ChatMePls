using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.User.Api.Authentication.UpdatePersonalData;


public record UpdatePersonalDataRequest(
    [Required] string Name,
    [Required] string Surname,
    [Required] string Gender,
    DateTime DateOfBirth
);


[ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class UpdatePersonalDataEndpoint(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UpdatePersonalData(UpdatePersonalDataRequest request)
    {
        if (ModelState.IsValid)
        {
            return ValidationProblem();
        }

        return null;
    }
}