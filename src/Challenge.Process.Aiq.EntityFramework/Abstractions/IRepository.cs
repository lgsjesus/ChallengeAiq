using System.Collections.ObjectModel;
using Challenge.Process.Aiq.Domain.Abstractions;
using Optional;

namespace Challenge.Process.Aiq.EntityFramework.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<Option<TEntity>> GetByIdAsync(long id); 
    Task<ReadOnlyCollection<TEntity>> GetAllAsync(Pagination pagination);
    Task DeleteEntityAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity); 
    Task<TEntity> CreateAsync(TEntity entity); 
}