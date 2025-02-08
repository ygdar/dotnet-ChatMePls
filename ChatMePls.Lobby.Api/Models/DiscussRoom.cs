using ChatMePls.Lobby.Api.Enums;
using Marten.Schema;

namespace ChatMePls.Lobby.Api.Models;

public class DiscussRoom
{
    [Identity]
    public Guid Uid { get; set; }

    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DiscussRoomStatus Status { get; set; }
    
    public DiscussRoomType Type { get; set; }
}