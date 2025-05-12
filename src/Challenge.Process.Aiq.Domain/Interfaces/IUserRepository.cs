using Challenge.Process.Aiq.Domain.Entities;

namespace Challenge.Process.Aiq.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string email, string password);
    Task CreateUserAsync(string email, string password);
    Task<bool> ExistsByEmailAsync(string email);
}