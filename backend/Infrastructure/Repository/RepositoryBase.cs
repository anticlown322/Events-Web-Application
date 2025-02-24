using System.Linq.Expressions;
using Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
        => RepositoryContext = repositoryContext;

    public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges, CancellationToken cancellationToken) =>
        await (!trackChanges
            ? RepositoryContext.Set<T>()
                .AsNoTracking()
            : RepositoryContext.Set<T>())
                .ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,
        bool trackChanges, CancellationToken cancellationToken) =>
        await (!trackChanges
            ? RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
            : RepositoryContext.Set<T>()
                .Where(expression))
            .ToListAsync(cancellationToken);

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}