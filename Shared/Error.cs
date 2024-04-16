namespace Shared;

public class Error(string code, string message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public string Code { get; } = code;

    public string Message { get; } = message;
}