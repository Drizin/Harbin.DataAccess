using System;
using System.Data;

namespace Harbin.DataAccess.Connections
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        Func<IDbConnection> _createConn;
        public DbConnectionFactory(Func<IDbConnection> createConn)
        {
            _createConn = createConn;
        }
        public IDbConnection CreateConnection()
        {
            return _createConn();
        }
    }
}
