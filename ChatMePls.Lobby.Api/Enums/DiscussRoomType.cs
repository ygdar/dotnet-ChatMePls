namespace ChatMePls.Lobby.Api.Enums;

[Flags]
public enum DiscussRoomType
{
    Public = 0x0001,
    Anonymous = 0x0002,
    ReadOnly = 0x0004,
    Pinned = 0x0008,
}