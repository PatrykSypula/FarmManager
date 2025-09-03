using System.IO;

namespace FarmManager.App.Helpers;

public static class FileHelper
{
    private const string _fileName = "load.txt";

    public static void Write(string id)
    {
        File.WriteAllText(_fileName, id);
    }
    public static string Read()
    {
        try
        {
            return File.ReadAllText(_fileName);
        }
        catch
        {
            return "1";
        }
    }
}
