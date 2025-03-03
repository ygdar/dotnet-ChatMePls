namespace ChatMePls.User.Api.Exceptions;

[Serializable]
public class UserNotFoundException(string UserUid) : Exception
{
}