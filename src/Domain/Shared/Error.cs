using Domain.Enums;

namespace Domain.Shared;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    protected Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    /// <summary>
    /// Cria um erro de validação (400 Bad Request)
    /// </summary>
    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    /// <summary>
    /// Cria um erro de não encontrado (404 Not Found)
    /// </summary>
    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Cria um erro de conflito (409 Conflict)
    /// </summary>
    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    /// <summary>
    /// Cria um erro de falha genérica (400 Bad Request)
    /// </summary>
    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static implicit operator string(Error error) => error.Code;
}