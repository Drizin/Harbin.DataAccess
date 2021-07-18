﻿using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Harbin.DataAccess.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class ReadDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
        // Internally we use Dapper FastCRUD to get table name

        #region Members
        protected readonly IReadDbConnection _db;
        protected string _tableName;
        #endregion Members

        #region ctors
        public ReadDbRepository(IReadDbConnection db)
        {
            _db = db;
            _tableName = Dapper.FastCrud.OrmConfiguration.GetSqlBuilder<TEntity>().GetTableName();
        }
        #endregion

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> QueryAll()
        {
            return _db.Query<TEntity>($"SELECT * FROM {_tableName:raw}");
        }

        #region AdjustSql
        /// <summary>
        /// If "SELECT" keyword is not provided, "SELECT * FROM tableName" will be automatically appended before your query, so you can pass only your where conditions.
        /// In this case if you omit the WHERE it will also be appended automatically.
        /// </summary>
        protected string AdjustSql(string sql)
        {
            if (!sql.ToUpper().TrimStart().StartsWith("SELECT"))
                sql = $"SELECT * FROM {_tableName} " + (sql.ToUpper().Contains("WHERE") ? "" : "WHERE ") + sql;
            return sql;
        }
        #endregion

        #region IReadDbConnection (Dapper Query) Facades
        #region IReadDbConnection.Query<TEntity> -> Dapper.Query<TEntity>

        /// <inheritdoc/>
        public IEnumerable<TEntity> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.Query<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public TEntity QueryFirst(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QueryFirst<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public TEntity QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QueryFirstOrDefault<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public TEntity QuerySingle(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QuerySingle<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public TEntity QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QuerySingleOrDefault<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region IReadDbConnection.QueryAsync<TEntity> -> Dapper.QueryAsync<TEntity>

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QueryAsync<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public Task<TEntity> QueryFirstAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QueryFirstAsync<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QueryFirstOrDefaultAsync<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public Task<TEntity> QuerySingleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QuerySingleAsync<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <inheritdoc/>
        public Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _db.QuerySingleOrDefaultAsync<TEntity>(sql: AdjustSql(sql), param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion
        #endregion

        #region DapperQueryBuilder
        /// <inheritdoc/>
        public virtual DapperQueryBuilder<TEntity> QueryBuilder()
        {
            return new DapperQueryBuilder<TEntity>(_db, $"SELECT * FROM {_tableName:raw} /**where**/");
        }

        /// <inheritdoc/>
        public virtual DapperQueryBuilder<TEntity> QueryBuilder(FormattableString query)
        {
            return new DapperQueryBuilder<TEntity>(_db, query);
        }
        #endregion


        #region Dapper.FastCRUD specific methods

        /// <inheritdoc/>
        public virtual TEntity SingleOrDefault(TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.Get(_db, entityKeys, statementOptions);
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> SingleOrDefaultAsync(TEntity entityKeys, Action<ISelectSqlSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.GetAsync(_db, entityKeys, statementOptions);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> Query(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions)
        {
            return Dapper.FastCrud.DapperExtensions.Find(_db, statementOptions);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TEntity>> QueryAsync(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity>> statementOptions)
        {
            return Dapper.FastCrud.DapperExtensions.FindAsync(_db, statementOptions);
        }

        /// <inheritdoc/>
        public virtual int Count(Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.Count(_db, statementOptions);
        }

        /// <inheritdoc/>
        public virtual Task<int> CountAsync(Action<IConditionalSqlStatementOptionsBuilder<TEntity>> statementOptions = null)
        {
            return Dapper.FastCrud.DapperExtensions.CountAsync(_db, statementOptions);
        }

        #endregion


    }
}
