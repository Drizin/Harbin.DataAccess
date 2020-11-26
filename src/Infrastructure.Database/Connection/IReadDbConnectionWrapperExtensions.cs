using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Connection
{
    /// <summary>
    /// Extends IReadDbConnectionWrapper with Dapper-Facades which will be executed over IReadDbConnection or (if IReadDbConnection is null) will invoke Dapper-Facades directly
    /// </summary>
    public static class IReadDbConnectionWrapperExtensions
    {
        #region Facade-Proxys to IReadDbConnection
        #region Dapper (facades Query<T>)
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static IEnumerable<T> Query<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.Query<T>(sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.Query<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static T QueryFirst<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirst<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirst<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static T QueryFirstOrDefault<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefault<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefault<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static T QuerySingle<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingle<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingle<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static T QuerySingleOrDefault<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingleOrDefault<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingleOrDefault<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region Dapper (facades Query() dynamic)
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static IEnumerable<dynamic> Query(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.Query(sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.Query(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static dynamic QueryFirst(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirst(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirst(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static dynamic QueryFirstOrDefault(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefault(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefault(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static dynamic QuerySingle(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingle(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingle(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region Dapper (facades Query<object>())
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static IEnumerable<object> Query(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.Query(type: type, sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.Query(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static object QueryFirst(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirst(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirst(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static object QueryFirstOrDefault(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefault(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefault(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static object QuerySingle(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingle(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingle(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region Dapper (facades QueryAsync<T>)
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryAsync<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryAsync<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static Task<T> QueryFirstAsync<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstAsync<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstAsync<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static Task<T> QueryFirstOrDefaultAsync<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefaultAsync<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefaultAsync<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static Task<T> QuerySingleAsync<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingleAsync<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingleAsync<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as T.
        /// </summary>
        public static Task<T> QuerySingleOrDefaultAsync<T>(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingleOrDefaultAsync<T>(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingleOrDefaultAsync<T>(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region Dapper (facades QueryAsync() dynamic)
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static Task<IEnumerable<dynamic>> QueryAsync(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryAsync(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static Task<dynamic> QueryFirstAsync(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstAsync(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static Task<dynamic> QueryFirstOrDefaultAsync(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefaultAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefaultAsync(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as dynamic objects.
        /// </summary>
        public static Task<dynamic> QuerySingleAsync(this IReadDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingleAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingleAsync(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region Dapper (facades QueryAsync<object>)
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static Task<IEnumerable<object>> QueryAsync(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryAsync(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryAsync(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static Task<object> QueryFirstAsync(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstAsync(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstAsync(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static Task<object> QueryFirstOrDefaultAsync(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QueryFirstOrDefaultAsync(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QueryFirstOrDefaultAsync(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        /// <summary>
        /// Executes the query (using Dapper), returning the data typed as type.
        /// </summary>
        public static Task<object> QuerySingleAsync(this IReadDbConnectionWrapper _dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadDbConnection readWriteDbConnection = _dbConnection.ReadDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.QuerySingleAsync(type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.QuerySingleAsync(_dbConnection.DbConnection, type: type, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #endregion
    }
}
