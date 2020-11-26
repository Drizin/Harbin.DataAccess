using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.Infrastructure.Database.Connection
{
    /// <summary>
    /// Any class that wraps an IReadWriteDbConnection (instead of inheriting from it) 
    /// can still rely on the IReadWriteDbConnection methods (which are Dapper facades) through extensions.
    /// </summary>
    public interface IReadWriteDbConnectionWrapper
    {
        IReadWriteDbConnection ReadWriteDbConnection { get; }
        IDbConnection DbConnection { get; } // should always have an underlying connection, which will be used as fallback if parent class was constructed with IDbConnection
    }
}
