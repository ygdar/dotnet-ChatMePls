using ChatMePls.DefaultServices.CQRS;
using ChatMePls.Lobby.Api.Enums;
using ChatMePls.Lobby.Api.Models;

namespace ChatMePls.Lobby.Api.DiscussRoom.CreateDiscussRoom;

public record InsertDiscussRoomCommand(string Title, string Description, DiscussRoomType RoomType, DiscussRoomStatus RoomStatus)
    : ICommand<InsertDiscussRoomResult>;

public record InsertDiscussRoomResult(Guid Uid);

internal class InsertDiscussRoomCommandHandler : ICommandHandler<InsertDiscussRoomCommand, InsertDiscussRoomResult>
{
    public Task<InsertDiscussRoomResult> Handle(InsertDiscussRoomCommand command, CancellationToken cancellationToken)
    {
        var room = new Models.DiscussRoom
        {
            Uid = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            Status = command.RoomStatus,
            Type = command.RoomType
        };
        
        return Task.FromResult(new InsertDiscussRoomResult(room.Uid));
    }
}