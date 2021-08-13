using Harbin.DataAccess.DapperSimpleCRUD.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.DapperSimpleCRUD.Repositories
{
    /// <inheritdoc/>
    public class ReadDbRepository<TEntity> : IReadDbRepository<TEntity>
    {
        // Internally we use Dapper SimpleCRUD to get table name

        #region Members
        protected readonly IReadDbConnection _db;
        protected string _tableName;
        #endregion Members

        #region ctors
        public ReadDbRepository(IReadDbConnection db)
        {
            _db = db;
            _tableName = (string) typeof(Dapper.SimpleCRUD)
                .GetMethod("GetTableName", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(Type) }, null)
                .Invoke(null, new object[] { typeof(TEntity) });
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


        #region Dapper.SimpleCRUD specific methods

        /// <inheritdoc/>
        public virtual TEntity FirstOrDefault(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.Get<TEntity>(_db, id, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> FirstOrDefaultAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetAsync<TEntity>(_db, id, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> GetList()
        {
            return Dapper.SimpleCRUD.GetList<TEntity>(_db);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> GetList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetList<TEntity>(_db, conditions, parameters, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> GetList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetList<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TEntity>> GetListAsync()
        {
            return Dapper.SimpleCRUD.GetListAsync<TEntity>(_db);
        }
        /// <inheritdoc/>
        public virtual Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetListAsync<TEntity>(_db, parameters, transaction, commandTimeout);
        }
        /// <inheritdoc/>
        public virtual Task<IEnumerable<TEntity>> GetListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetListAsync<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual int RecordCount(string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.RecordCount<TEntity>(_db, conditions, parameters, transaction, commandTimeout);
        }
        /// <inheritdoc/>
        public virtual int RecordCount(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.RecordCount<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual Task<int> RecordCountAsync(string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.RecordCountAsync<TEntity>(_db, conditions, parameters, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public virtual Task<int> RecordCountAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.RecordCountAsync<TEntity>(_db, whereConditions, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetListPaged<TEntity>(_db, pageNumber, rowsPerPage, conditions, orderby, parameters, transaction, commandTimeout);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Dapper.SimpleCRUD.GetListPagedAsync<TEntity>(_db, pageNumber, rowsPerPage, conditions, orderby, parameters, transaction, commandTimeout);
        }

        #endregion


    }
}
