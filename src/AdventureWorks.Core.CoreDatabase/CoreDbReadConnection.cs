using Harbin.Infrastructure.Database.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
