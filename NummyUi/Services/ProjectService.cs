using NummyUi.Models;
using NummyUi.Services.Abstract;

namespace NummyUi.Services;

public class ProjectService(HttpClient httpClient) : IProjectService
{
    public async Task<NoticeType[]> GetProjectNoticeAsync()
    {
        var result = await httpClient.GetFromJsonAsync<NoticeType[]>("data/notice.json");
        return result ?? [];
    }

    public async Task<NoticeItem[]> GetNoticesAsync()
    {
        var result = await httpClient.GetFromJsonAsync<NoticeItem[]>("data/notices.json");
        return result ?? [];
    }

    public async Task<ActivitiesType[]> GetActivitiesAsync()
    {
        var result = await httpClient.GetFromJsonAsync<ActivitiesType[]>("data/activities.json");
        return result ?? [];
    }

    public async Task<ListItemDataType[]> GetFakeListAsync(int count = 0)
    {
        var data = await httpClient.GetFromJsonAsync<ListItemDataType[]>("data/fake_list.json");
        if (data == null) return [];
        return count > 0 ? data.Take(count).ToArray() : data;
    }
}
