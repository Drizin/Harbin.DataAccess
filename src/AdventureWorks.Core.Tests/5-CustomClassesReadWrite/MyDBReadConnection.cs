using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connection;
using Harbin.DataAccess.Repositories;
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
