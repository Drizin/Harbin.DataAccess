using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class MyDBReadWriteConnection : ReadWriteDbConnection
    {
        protected MyDBReadWriteConnection() : base((IDbConnection)null) { } // Moq requires empty constructor
        public MyDBReadWriteConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        /// <summary>
        /// If there are reusable Query Objects or Query Methods in your ReadDbConnection, you can reuse those methods (with same connection)
        /// </summary>
        public virtual MyDBReadConnection MyDBReadConnection { get { return new MyDBReadConnection(_dbConnection); } }

        #region DbCommands
        public virtual void CalculateBestCustomers(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            this.Execute("UPDATE [Person].[Person] SET [FirstName]=[FirstName]", param: null, transaction: transaction, commandTimeout: commandTimeout);
        }
        #endregion
    }
}
