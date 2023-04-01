using Harbin.DataAccess.Connections;
using System.Data;

namespace AdventureWorks.Core.CoreDatabase
{
    public partial class CoreDbReadConnection : ReadDbConnection
    {
        public CoreDbReadConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
        public CoreDbReadConnection(IDbConnectionFactory connFactory) : this(connFactory.CreateConnection())
        {
        }
    }
}
