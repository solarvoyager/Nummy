using NummyShared.DTOs;

namespace NummyUi.Session;

public interface IUserSession
{
    void SetUser(UserToListDto user);
    UserToListDto? GetUser();
    bool IsLoggedIn();
    void Logout();
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

    public bool IsLoggedIn()
    {
        return User != null;
    }

    public void Logout()
    {
        User = null;
    }
}