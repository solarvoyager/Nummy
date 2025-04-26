using AntDesign.ProLayout;

namespace NummyUi.Utils;

public static class NummyContants
{
    public const string ClientName = "NummyApiClient";
    public const string GitHubUrl = "https://github.com/orgs/solarvoyager";
    
    public const string GetPendingMigrationsUrl = "api/database/migrations/pending";
    public const string ApplyPendingMigrationsUrl = "api/database/migrations/pending/apply";
    public const string GetCodeLogsUrl = "api/log/get/code";
    public const string GetRequestLogsUrl = "api/log/get/request";
    public const string GetResponseLogsUrl = "api/log/get/response";
    public const string DeleteCodeLogsUrl = "api/log/delete/code";
    public const string GetCodeLogsByTraceIdentifierUrl = "api/log/get/code";
    public const string GetTotalCountsUrl = "api/statistical/total";
    public const string GetDsnUrl = "api/helper/dsn";
    
    public const string GetUserUrl = "api/user";
    public const string LoginUrl = "api/user/login";
    public const string RegisterUrl = "api/user/register";

    public const string GetTeamsUrl = "api/team";
    public const string AddTeamUrl = "api/team";
    public const string UpdateTeamUrl = "api/team";
    public const string DeleteTeamUrl = "api/team";
    public const string AddUserToTeamUrl = "api/team";
    public const string TeamShareUrl = "/team";

    public static readonly int[] PageSizes = [5, 10, 20, 50, 100, 1000];

    public static readonly MenuDataItem[] MenuDataItems =
    [
        new()
        {
            Path = "/",
            Name = "Dashboard",
            Key = "dashboard",
            Icon = "dashboard"
        },
        new()
        {
            Path = "/http",
            Name = "Http Logs",
            Key = "http",
            Icon = "swap"
        },
        new()
        {
            Path = "/code",
            Name = "Code Logs",
            Key = "code",
            Icon = "node-index"
        },
        new()
        {
            Path = "/application",
            Name = "Applications (in dev)",
            Key = "application",
            Icon = "appstore"
        },
        new()
        {
            Path = "/team",
            Name = "Teams (in dev)",
            Key = "team",
            Icon = "team"
        },
        new()
        {
            Path = "/configuration",
            Name = "Configuration (in dev)",
            Key = "configuration",
            Icon = "setting"
        },
    ];
    
    public static readonly Dictionary<string, string> AccountMenuItems = new()
    {
        {"account", "Account Settings"},
        {"security", "Security Settings"},
        {"binding", "Account Binding"},
        {"notification", "New Message Notification"},
    };

    public const string AccountMenuDefaultItemKey = "account";
}
