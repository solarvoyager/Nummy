namespace NummyUi.Utils;

public class NummyContants
{
    public const string ClientName = "NummyApiClient";
    public const string GetPendingMigrationsUrl = "api/database/migrations/pending";
    public const string ApplyPendingMigrationsUrl = "api/database/migrations/pending/apply";
    public const string GetCodeLogsUrl = "api/log/get/code";
    public const string GetRequestLogsUrl = "api/log/request";
    public const string GetResponseLogsUrl = "api/log/response";

    public static readonly int[] PageSizes = [5, 10, 20, 50, 100, 1000];
}