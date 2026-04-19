namespace FarmManager.Model.Exceptions;
public class DeletionResult
{
    public bool DidDelete { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}
