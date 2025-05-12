using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Process.Aiq.EntityFramework.Repositories;

public sealed class UserRepository(ChallengeProcessAiqDbContext context, IUnitOfWork unitOfWork) 
    : Repository<User>(context, unitOfWork), IUserRepository
{
    public async Task<User?> GetUserAsync(string email, string password)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(u => 
            string.Equals(u.Email , email) && string.Equals(u.Password, password));
    }
    public async Task CreateUserAsync(string email, string password)
    {
        await CreateAsync(User.Create(email, password));
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await DbSet.AsNoTracking().AnyAsync(u =>string.Equals(u.Email , email));
    }
}