using ChatMePls.Lobby.Api.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.Lobby.Api.DiscussRoom.CreateDiscussRoom;

public record CreateDiscussRoomRequest(string Name, string Description);

public record CreateDiscussRoomResponse(Guid Uid);

[ApiController]
[Route("[controller]")]
public class CreateDiscussRoomEndpoint : ControllerBase
{
    private readonly ISender _sender;

    public CreateDiscussRoomEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscussRoom(CreateDiscussRoomRequest request)
    {
        var command = new InsertDiscussRoomCommand(
            request.Name,
            request.Description,
            RoomType: DiscussRoomType.Public | DiscussRoomType.ReadOnly,
            RoomStatus: DiscussRoomStatus.Draft);
        
        var result = await _sender.Send(command);
        
        var response = new CreateDiscussRoomResponse(result.Uid);
        
        return Ok(response);
    }
}