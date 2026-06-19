namespace Dsw2026Ej15.Api;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}