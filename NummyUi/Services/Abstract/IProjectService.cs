using NummyUi.Models;

namespace NummyUi.Services.Abstract;

public interface IProjectService
{
    Task<NoticeType[]> GetProjectNoticeAsync();
    Task<ActivitiesType[]> GetActivitiesAsync();
    Task<ListItemDataType[]> GetFakeListAsync(int count = 0);
    Task<NoticeItem[]> GetNoticesAsync();
}
