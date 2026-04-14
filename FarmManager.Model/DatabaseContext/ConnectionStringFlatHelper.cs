namespace FarmManager.Model.DatabaseContext;

public class ConnectionStringFlatHelper
{
    public static string ReadConnectionString(string filename)
    {
        try
        {
            return File.ReadAllText(filename);
        }
        catch
        {
            return "Host=localhost;Port=5432;Database=FarmManager;Username=postgres;Password=admin";
        }
    }
}
