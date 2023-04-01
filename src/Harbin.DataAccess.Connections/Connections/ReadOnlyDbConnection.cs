using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// The underlying IDbConnection can be a read-only connection (for safety) and/or can point to read-replicas.
    /// </summary>
    public class ReadOnlyDbConnection : ReadDbConnection, IDbConnection
    {
        /// <inheritdoc/>
        public ReadOnlyDbConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
