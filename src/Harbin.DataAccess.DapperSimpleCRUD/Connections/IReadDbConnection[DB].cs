using Harbin.DataAccess.DapperSimpleCRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperSimpleCRUD.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public interface IReadDbConnection<DB> : IReadDbConnection
    {
        #region GetReadRepository<T>
        /// <summary>
        /// Get a Repository which you know that resides in this physical database
        /// Instead of this you can also create extensions methods to access all repositories which belong to a given physical database, like this:
        /// public static IReadDbRepository<YourEntity> GetYourEntityRepository(this IReadDbConnection<YourDatabase> db) => new ReadDbRepository<YourEntity, YourDatabase>(db);
        /// </summary>
        IReadDbRepository<TEntity, DB> GetReadRepository<TEntity>();
        #endregion
    }
}
