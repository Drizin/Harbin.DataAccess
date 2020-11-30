using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Repositories encapsulate storage/retrieval/search of objects.
    /// 
    /// IReadDbRepository{TEntity} defines generic operations 
    /// like QueryAll (returns all records) or Query (you can build your own SQL query).
    /// 
    /// This Repository is a Persistence-Based repository,
    /// as described by Vaughn Vernon in "Implementing Domain-Driven Design" book, 
    /// where he makes a distinction between  collection-based repositories and persistence-based repositories.
    /// 
    /// This does NOT emulate a collection of objects, as it was described by Eric Evans in "Domain-Driven Design" book.
    /// 
    /// Since the Repository wraps classes which are a direct representation of our tables it could also be called DAO.
    /// 
    /// ReadDbRepository{TEntity} can be used in multiple ways:
    /// - You can use ReadDbRepository{TEntity} and use "Query" methods by providing your filters
    /// - You can extend it using extension methods to create reusable Queries methods (or QueryObjects)
    /// - You can extend it using inheritance to create reusable Queries (or QueryObjects) which can be overriden in derived Mock classes.
    /// </summary>
    public interface IReadDbRepository<TEntity>
    {
        /// <summary>
        /// Returns all records. SELECT * FROM tableName.
        /// </summary>
        IEnumerable<TEntity> QueryAll();

        /// <summary>
        /// Builds a DapperQueryBuilder, initialized with "SELECT * FROM tableName", 
        /// where you can append your conditions using interpolated strings which are converted into SqlParameters.
        /// </summary>
        DapperQueryBuilder<TEntity> QueryBuilder();

        /// <summary>
        /// Builds a DapperQueryBuilder, initialized with an initial query, 
        /// where you can append your conditions using interpolated strings which are converted into SqlParameters.
        /// </summary>
        DapperQueryBuilder<TEntity> QueryBuilder(FormattableString query);

        #region IReadDbConnection.Query<TEntity> -> Dapper.Query<TEntity>
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        IEnumerable<TEntity> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QueryFirst(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QuerySingle(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        TEntity QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        #endregion

        #region IReadDbConnection.QueryAsync<TEntity> -> Dapper.QueryAsync<TEntity>

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// Queries can be like "SELECT * FROM TableName WHERE conditions", or "WHERE conditions", or just "conditions".
        /// (SQL is automatically adjusted if you don't provide "SELECT" or "WHERE" keywords)
        /// </summary>
        Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// Queries can be like "SELECT * FROM TableName WHERE conditions", or "WHERE conditions", or just "conditions".
        /// (SQL is automatically adjusted if you don't provide "SELECT" or "WHERE" keywords)
        /// </summary>
        Task<TEntity> QueryFirstAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// Queries can be like "SELECT * FROM TableName WHERE conditions", or "WHERE conditions", or just "conditions".
        /// (SQL is automatically adjusted if you don't provide "SELECT" or "WHERE" keywords)
        /// </summary>
        Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// Queries can be like "SELECT * FROM TableName WHERE conditions", or "WHERE conditions", or just "conditions".
        /// (SQL is automatically adjusted if you don't provide "SELECT" or "WHERE" keywords)
        /// </summary>
        Task<TEntity> QuerySingleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// Queries can be like "SELECT * FROM TableName WHERE conditions", or "WHERE conditions", or just "conditions".
        /// (SQL is automatically adjusted if you don't provide "SELECT" or "WHERE" keywords)
        /// </summary>
        Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        #endregion

        #region Dapper.FastCRUD specific methods

        /// <summary>
        /// Searches by TEntity entityKeys which should filter your record.
        /// In  Dapper FastCRUD this is known as "Get", in some other Repository implementations this is known as "GetById". 
        /// </summary>
        TEntity SingleOrDefault(TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Searches by TEntity entityKeys which should filter your record.
        /// In  Dapper FastCRUD this is known as "Get", in some other Repository implementations this is known as "GetById". 
        /// </summary>
        Task<TEntity> SingleOrDefaultAsync(TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Searches records by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        IEnumerable<TEntity> Query(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions);

        /// <summary>
        /// Searches records by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        Task<IEnumerable<TEntity>> QueryAsync(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions);

        /// <summary>
        /// Count records by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        int Count(Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        /// <summary>
        /// Count records by passing a query statement using Dapper FastCRUD syntax.
        /// </summary>
        Task<int> CountAsync(Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null);

        #endregion


    }
}
