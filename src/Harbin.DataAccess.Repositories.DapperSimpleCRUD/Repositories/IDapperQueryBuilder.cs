using DapperQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Repositories.DapperSimpleCRUD
{
    public interface IDapperQueryBuilder<TEntity>
    {
        DapperQueryBuilder<TEntity> Append(FormattableString statement);
        DapperQueryBuilder<TEntity> AppendLine(FormattableString statement);
        IEnumerable<TEntity> Query(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        Task<IEnumerable<TEntity>> QueryAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QueryFirst(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QueryFirstAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QueryFirstOrDefault(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QueryFirstOrDefaultAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QuerySingle(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QuerySingleAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QuerySingleOrDefault(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QuerySingleOrDefaultAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        DapperQueryBuilder<TEntity> Where(Filter filter);
        DapperQueryBuilder<TEntity> Where(Filters filters);
        DapperQueryBuilder<TEntity> Where(FormattableString filter);
    }
}