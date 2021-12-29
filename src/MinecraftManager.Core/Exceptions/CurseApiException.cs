namespace MinecraftManager.Core.Exceptions;

public class CurseApiException : Exception
{
    public CurseApiException() { }
    public CurseApiException(string message) : base(message) { }
    public CurseApiException(string message, Exception inner) : base(message, inner) { }
}