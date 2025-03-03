using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.User.Api.Authentication.GetPersonalData;

public class GetPersonalDataResponse
{
    public Guid Uid { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Gender { get; init; }
    public string DateOfBirth { get; init; }
}

[ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class GetPersonalDataEndpoint(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<GetPersonalDataResponse> GetPersonalData()
    {
        var userUid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetPersonalDataRequest(userUid);
        
        var response = await sender.Send(query);

        return new GetPersonalDataResponse
        {
            Uid = response.Uid,
            Email = response.Email,
            Name = response.Name,
            Surname = response.Surname,
            Gender = response.Gender,
            DateOfBirth = response.DateOfBirth,
        };
    }
}