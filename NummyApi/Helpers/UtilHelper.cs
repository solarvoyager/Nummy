namespace NummyApi.Helpers;

public static class UtilHelper
{
    public static string GenerateRandomColorHex()
    {
        var random = new Random();
        var color = $"#{random.Next(0x1000000):X6}";
        return color;
    }
}