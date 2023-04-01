namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    /// <inheritdoc/>
    public interface IReadWriteDbRepository<TEntity, DB> : IReadWriteDbRepository<TEntity>
    {
        //void Insert(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
        //void Update(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
        //void Save(TEntity e, IDbTransaction transaction = null, int? commandTimeout = null);
    }
}
