using ChatMePls.Lobby.Api.Enums;
using Marten.Schema;

namespace ChatMePls.Lobby.Api.Models;

public class Room
{
    [Identity]
    public Guid Uid { get; set; }
    
    public Guid RoomUid { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public IList<Guid> Admins { get; set; } = new List<Guid>();
    
    public IList<Guid> Participants { get; set; } = new List<Guid>();
    
    public PrivacyLevel ReadingLevel { get; set; }
    
    public PrivacyLevel PostingLevel { get; set; }
}