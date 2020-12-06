using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DapperQueryBuilder;
using Harbin.DataAccess.Connection;

namespace Harbin.DataAccess.Repositories
{
    /// <summary>
    /// Wraps DapperQueryBuilder library but simplifying the Query extensions to show only the extensions which return the type TEntity 
    /// of the repository entity
    /// </summary>
    public class DapperQueryBuilder<TEntity>
    {
        protected readonly IReadDbConnection _db;
        protected readonly DapperQueryBuilder.QueryBuilder _builder;
        public DapperQueryBuilder(IReadDbConnection db, FormattableString query)
        {
            _db = db;
            _builder = _db.QueryBuilder(query);
        }

        /// <summary>
        //     Appends a statement to the current command.
        //     Parameters embedded using string-interpolation will be automatically converted
        //     into Dapper parameters.
        /// </summary>
        public DapperQueryBuilder<TEntity> Append(FormattableString statement)
        {
            _builder.Append(statement);
            return this;
        }

        /// <summary>
        //     Appends a statement to the current command, but before statement adds a linebreak.
        //     Parameters embedded using string-interpolation will be automatically converted
        //     into Dapper parameters.
        /// </summary>
        public DapperQueryBuilder<TEntity> AppendLine(FormattableString statement)
        {
            _builder.AppendLine(statement);
            return this;
        }

        /// <summary>
        //     Adds a new condition to where clauses.
        /// </summary>
        public virtual DapperQueryBuilder<TEntity> Where(Filter filter)
        {
            _builder.Where(filter);
            return this;
        }

        /// <summary>
        //     Adds a new condition to where clauses.
        /// </summary>
        public virtual DapperQueryBuilder<TEntity> Where(Filters filters)
        {
            _builder.Where(filters);
            return this;
        }

        /// <summary>
        //     Adds a new condition to where clauses.
        //     Parameters embedded using string-interpolation will be automatically converted
        //     into Dapper parameters.
        /// </summary>
        public virtual DapperQueryBuilder<TEntity> Where(FormattableString filter)
        {
            _builder.Where(filter);
            return this;
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual IEnumerable<TEntity> Query(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.Query<TEntity>(transaction, buffered, commandTimeout, commandType);
        }


        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual Task<IEnumerable<TEntity>> QueryAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QueryAsync<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual TEntity QueryFirst(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QueryFirst<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual Task<TEntity> QueryFirstAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QueryFirstAsync<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual TEntity QueryFirstOrDefault(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QueryFirstOrDefault<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual Task<TEntity> QueryFirstOrDefaultAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QueryFirstOrDefaultAsync<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual TEntity QuerySingle(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QuerySingle<TEntity>(transaction, commandTimeout, commandType);
        }

        /// <summary>
        //     Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public virtual Task<TEntity> QuerySingleAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _builder.QuerySingleAsync<TEntity>(transaction, commandTimeout, commandType);
        }

    }
}
