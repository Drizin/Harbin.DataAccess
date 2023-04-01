using Harbin.DataAccess.Connections;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    public static class ConnectionExtensions
    {
        #region Repository<TEntity> Factory

        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories"/> </summary>
        public static IReadWriteDbRepository<TEntity> GetReadWriteRepository<TEntity>(this IReadWriteDbConnection conn)
            => DefaultFactories.ReadWriteDbRepositoryFactory.Create<TEntity>(conn);

        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories"/> </summary>
        public static IReadOnlyDbRepository<TEntity> GetReadOnlyRepository<TEntity>(this IReadOnlyDbConnection conn)
            => DefaultFactories.ReadOnlyDbRepositoryFactory.Create<TEntity>(conn);

        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories"/> </summary>
        public static IReadDbRepository<TEntity> GetReadRepository<TEntity>(this IReadDbConnection conn)
            => DefaultFactories.ReadDbRepositoryFactory.Create<TEntity>(conn);


        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories{DB}"/> </summary>
        public static IReadWriteDbRepository<TEntity, DB> GetReadWriteRepository<TEntity, DB>(this IReadWriteDbConnection<DB> conn)
            => DefaultFactories<DB>.ReadWriteDbRepositoryFactory.Create<TEntity>(conn);

        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories{DB}"/> </summary>
        public static IReadOnlyDbRepository<TEntity, DB> GetReadOnlyRepository<TEntity, DB>(this IReadOnlyDbConnection<DB> conn)
            => DefaultFactories<DB>.ReadOnlyDbRepositoryFactory.Create<TEntity>(conn);

        /// <summary>Creates Repository using the factories defined in <see cref="DefaultFactories{DB}"/> </summary>
        public static IReadDbRepository<TEntity, DB> GetReadRepository<TEntity, DB>(this IReadDbConnection<DB> conn)
            => DefaultFactories<DB>.ReadDbRepositoryFactory.Create<TEntity>(conn);
        #endregion
    }
}
