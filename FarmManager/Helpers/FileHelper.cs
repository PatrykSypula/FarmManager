using System.IO;

namespace FarmManager.App.Helpers;

public static class FileHelper
{

    public static void Write(string filename, string data)
    {
        File.WriteAllText(filename, data);
    }
    public static string Read(string filename)
    {
        try
        {
            return File.ReadAllText(filename);
        }
        catch
        {
            return "";
        }
    }
}
