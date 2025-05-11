using System.Collections.ObjectModel;
using Challenge.Process.Aiq.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Challenge.Process.Aiq.EntityFramework.Abstractions;

public class Repository<TEntity>(ChallengeProcessAiqDbContext context, IUnitOfWork unitOfWork) 
    : IRepository<TEntity> where TEntity : Entity
{
    protected DbSet<TEntity> DbSet => context.Set<TEntity>();
    
    public async Task<bool> ExistsByIdAsync(long id) => await DbSet.AsNoTracking().AnyAsync(e => e.Id == id);
    public async Task<Option<TEntity>> GetByIdAsync(long id)
    {
        var item = await DbSet.AsNoTracking().FirstOrDefaultAsync(i=> i.Id == id);
        return item == null ? Option.None<TEntity>() : Option.Some(item);
    }
    public async Task<ReadOnlyCollection<TEntity>> GetAllAsync(Pagination pagination)
    {
        var pagedData = await DbSet.AsNoTracking()
            .OrderBy(e => e.Id) 
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        return  pagedData.Count == 0? ReadOnlyCollection<TEntity>.Empty : pagedData.AsReadOnly();
    }
    public async Task DeleteEntityAsync(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }
        DbSet.Remove(entity);
        await unitOfWork.Commit();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        await unitOfWork.Commit();
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await unitOfWork.Commit();
        return entity;
    }
}