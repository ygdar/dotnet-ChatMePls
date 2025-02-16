using ChatMePls.Lobby.Api.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatMePls.Lobby.Api.Room.CreateRoom;

public record CreateDiscussRoomRequest(string Name, string Description);

public record CreateDiscussRoomResponse(Guid Uid);

[ApiController]
[Route("[controller]")]
public class CreateRoomEndpoint(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDiscussRoom(CreateDiscussRoomRequest request)
    {
        var command = new InsertDiscussRoomCommand(
            request.Name,
            request.Description,
            RoomType: DiscussRoomType.Public | DiscussRoomType.ReadOnly,
            RoomStatus: DiscussRoomStatus.Draft);
        
        var result = await sender.Send(command);
        
        var response = new CreateDiscussRoomResponse(result.Uid);
        
        return Ok(response);
    }
}