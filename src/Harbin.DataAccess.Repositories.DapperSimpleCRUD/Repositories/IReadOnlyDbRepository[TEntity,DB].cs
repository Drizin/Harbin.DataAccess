namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public interface IReadOnlyDbRepository<TEntity, DB> : IReadDbRepository<TEntity>, IReadDbRepository<TEntity, DB>, IReadOnlyDbRepository<TEntity>
    {
    }
}
