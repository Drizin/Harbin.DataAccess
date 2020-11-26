using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.Infrastructure.Database.Connection
{
    public static class IReadWriteDbConnectionWrapperExtensions
    {
        #region Facade-Proxys to IReadWriteDbConnection
        #region Dapper (facades Execute())
        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public static int Execute(this IReadWriteDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadWriteDbConnection readWriteDbConnection = _dbConnection.ReadWriteDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.Execute(sql: sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.Execute(_dbConnection.DbConnection, sql: sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public static Task<int> ExecuteAsync(this IReadWriteDbConnectionWrapper _dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IReadWriteDbConnection readWriteDbConnection = _dbConnection.ReadWriteDbConnection;
            if (readWriteDbConnection != null)
                return readWriteDbConnection.ExecuteAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            return Dapper.SqlMapper.ExecuteAsync(_dbConnection.DbConnection, sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion
        #endregion
    }
}
