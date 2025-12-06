using System.IO;

namespace FarmManager.App.Helpers;

public static class FileHelper
{
    private static readonly string BasePath;

    static FileHelper()
    {
        BasePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "FarmManager"
        );

        Directory.CreateDirectory(BasePath);
    }

    public static string Write(string filename, string data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(BasePath, filename))!);
            File.WriteAllText(Path.Combine(BasePath, filename), data);
            return "";
        }
        catch
        {
            return "";
        }
    }

    public static string Read(string filename)
    {
        try
        {
            return File.Exists(Path.Combine(BasePath, filename)) ? File.ReadAllText(Path.Combine(BasePath, filename)) : "";
        }
        catch
        {
            return "";
        }
    }
}
