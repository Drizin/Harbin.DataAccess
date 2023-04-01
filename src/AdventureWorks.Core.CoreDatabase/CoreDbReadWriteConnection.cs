using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Data;

namespace AdventureWorks.Core.CoreDatabase
{
    public partial class CoreDbReadWriteConnection : ReadWriteDbConnection
    {
        public CoreDbReadWriteConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
        public CoreDbReadWriteConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }

        /// <summary>
        /// If there are reusable Query Objects or Query Methods in your ReadDbConnection, you can reuse those methods (with same connection)
        /// </summary>
        public virtual CoreDbReadConnection CoreDbReadConnection { get { return new CoreDbReadConnection(_dbConnection); } }
    }
}
