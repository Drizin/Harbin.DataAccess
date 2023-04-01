using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (Queries) and Dapper Execute extensions (Commands).
    /// </summary>
    public interface IReadWriteDbConnection : IReadDbConnection, IDbConnection
    {
        #region Dapper Execute()
        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        #endregion
    }
}
