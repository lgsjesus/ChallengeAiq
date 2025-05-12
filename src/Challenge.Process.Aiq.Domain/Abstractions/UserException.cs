namespace Challenge.Process.Aiq.Domain.Abstractions;

public sealed class UserException(string? message) : Exception(message);