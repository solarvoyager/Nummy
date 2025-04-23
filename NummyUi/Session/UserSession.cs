using NummyShared.Dtos;

namespace NummyUi.Session;

public interface IUserSession
{
    void SetUser(UserToListDto user);
    UserToListDto? GetUser();
}

public class UserSession : IUserSession
{
    private UserToListDto? User { get; set; }
    
    public void SetUser(UserToListDto user)
    {
        User = user;
    }

    public UserToListDto? GetUser()
    {
        return User;
    }
}