namespace Challenge.Process.Aiq.EntityFramework.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}