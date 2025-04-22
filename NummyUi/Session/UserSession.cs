using NummyShared.Dtos;

namespace NummyUi.Session;

public class UserSession
{
    public Guid? UserId { get; set; }
    public UserToListDto? User { get; set; }
}