﻿using DapperQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// </summary>
    public interface IReadDbConnection : IDbConnection
    {
        /// <summary>
        /// Underlying IDbConnection
        /// </summary>
        IDbConnection DbConnection { get; }

        #region Dapper Query[modifier]<TEntity>()
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QueryFirst<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QueryFirstOrDefault<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QuerySingle<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QuerySingleOrDefault<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        #endregion

        #region Dapper Query[modifier]Async<TEntity>()
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        Task<TEntity> QueryFirstAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        Task<TEntity> QuerySingleAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        Task<TEntity> QuerySingleOrDefaultAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        #endregion

        #region DapperQueryBuilder
        /// <summary>
        /// Creates a DapperQueryBuilder, which is a builder for dynamically building queries using string interpolation (but it's SQL-Injection safe).
        /// </summary>
        QueryBuilder QueryBuilder();

        /// <summary>
        /// Creates a DapperQueryBuilder, which is a builder for dynamically building queries using string interpolation (but it's SQL-Injection safe).
        /// </summary>
        QueryBuilder QueryBuilder(FormattableString query);
        #endregion
    }
}
