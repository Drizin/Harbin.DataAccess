using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Harbin.DataAccess.Connections
{
    /// <inheritdoc/>
    public interface IReadWriteDbConnection<DB> : IReadWriteDbConnection, IReadDbConnection<DB>
    {

    }
}
