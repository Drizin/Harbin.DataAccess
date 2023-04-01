using Harbin.DataAccess.Connections;
using System.Data;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class MyDBReadConnection : ReadDbConnection
    {
        protected MyDBReadConnection() : base((IDbConnection)null) { } // Moq requires empty constructor
        public MyDBReadConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
