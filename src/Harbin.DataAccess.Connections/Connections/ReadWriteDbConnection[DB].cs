using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (Queries) and Dapper Execute extensions (Commands).
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public class ReadWriteDbConnection<DB> : ReadOnlyDbConnection<DB>, IReadWriteDbConnection<DB>
    {
        /// <inheritdoc/>
        public ReadWriteDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        /// <inheritdoc/>
        public ReadWriteDbConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }

        /// <inheritdoc/>
        public ReadWriteDbConnection(IDbConnectionFactory<DB> connFactory) : this(connFactory.CreateConnection())
        {
        }

        #region Dapper (facades Execute())
        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.Execute(sql: sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        /// <summary>
        /// Executes the query (using Dapper), returning the number of rows affected.
        /// </summary>
        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.ExecuteAsync(sql: sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
        #endregion

        #region IDbConnection facades (composition) - overrides over ReadOnlyDbConnection
        public override void ChangeDatabase(string databaseName)
        {
            DbConnection.ChangeDatabase(databaseName);
        }
        public override IDbCommand CreateCommand()
        {
            return DbConnection.CreateCommand();
        }
        #endregion

    }
}
