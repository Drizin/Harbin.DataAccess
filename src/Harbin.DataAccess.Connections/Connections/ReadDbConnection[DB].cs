using System.Data;

namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions)
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public abstract class ReadDbConnection<DB> : ReadDbConnection, IReadDbConnection, IReadDbConnection<DB>, IDbConnection
    {
        /// <inheritdoc/>
        public ReadDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        /// <inheritdoc/>
        public ReadDbConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }

        /// <inheritdoc/>
        public ReadDbConnection(IDbConnectionFactory<DB> connFactory) : this(connFactory.CreateConnection())
        {
        }
    }
}
