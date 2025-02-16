using ChatMePls.DefaultServices.CQRS;
using ChatMePls.Lobby.Api.Enums;
using Marten;

namespace ChatMePls.Lobby.Api.Room.CreateRoom;

public record InsertDiscussRoomCommand(string Title, string Description, DiscussRoomType RoomType, DiscussRoomStatus RoomStatus)
    : ICommand<InsertDiscussRoomResult>;

public record InsertDiscussRoomResult(Guid Uid);

internal class InsertDiscussRoomCommandHandler(
    IDocumentSession documentSession)
    : ICommandHandler<InsertDiscussRoomCommand, InsertDiscussRoomResult>
{
    public async Task<InsertDiscussRoomResult> Handle(InsertDiscussRoomCommand command, CancellationToken cancellationToken)
    {
        var room = new Models.Room
        {
            Uid = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
        };
        
        documentSession.Store(room);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return new InsertDiscussRoomResult(room.Uid);
    }
}