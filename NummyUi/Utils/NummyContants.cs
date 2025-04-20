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

    public static readonly int[] PageSizes = [5, 10, 20, 50, 100, 1000];
}