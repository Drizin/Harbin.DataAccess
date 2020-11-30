using AdventureWorks.Core.Domain.Entities;
using Harbin.Infrastructure.Database.Connection;
using Harbin.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
