namespace SS.AuthService.Application.Common.Exceptions;

/// <summary>
/// Exception dasar untuk error pada layer Application yang diantisipasi (bukan 500 server error).
/// Digunakan untuk membedakan business rule violations dari unhandled system exceptions.
/// </summary>
public class AppException : Exception
{
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception innerException) : base(message, innerException) { }
}
