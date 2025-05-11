using System.Text;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.EntityFramework.Abstractions;

public sealed class UnitOfWork(ChallengeProcessAiqDbContext context) : IUnitOfWork
{
    private bool _disposed;
    private void Disposing(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            context.Dispose();
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Disposing(true);
        GC.SuppressFinalize(this);
    }
    
    public async Task<bool> Commit()
    {
        try
        {
            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            throw new UserException(GetFullErrorMessage(e));
        }
    }
    private static string GetFullErrorMessage(Exception e)
    {
        var errorMessage = new StringBuilder();
        errorMessage.AppendLine(e.Message);

        var innerException = e.InnerException;
        while (innerException != null)
        {
            if (innerException.InnerException == null)
                errorMessage.AppendLine(innerException.Message);
            innerException = innerException.InnerException;
        }
        return errorMessage.ToString();
    }
}