namespace Challenge.Process.Aiq.Domain.Abstractions;

public sealed class UserNotFoundException(string? message) : Exception(message);