using ChatMePls.Lobby.Api.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.Lobby.Api.House.CreateHouse;


public record CreateHouseRequest(
    string Title,
    string Description,
    PrivacyLevel ReadingPrivacyLevel,
    PrivacyLevel PostingPrivacyLevel);

public record CreateHouseResponse(Guid HouseUid);

[ApiController]
[Route("[controller]")]
public class CreateHouseEndpoint(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<CreateHouseResponse> CreateHouse(CreateHouseRequest request)
    {
        var command = new CreateHouseCommand(
            Title: request.Title,
            Description: request.Description,
            ReadingPrivacyLevel: request.ReadingPrivacyLevel,
            PostingPrivacyLevel: request.PostingPrivacyLevel,
            Admins: new List<Guid>{ Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });
        
        var result = await sender.Send(command);
        
        return new CreateHouseResponse(result.HouseUid);
    }
}