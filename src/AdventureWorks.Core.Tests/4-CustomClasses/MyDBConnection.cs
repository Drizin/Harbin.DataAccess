using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class MyDBConnection : ReadWriteDbConnection
    {
        protected MyDBConnection() : base((IDbConnection)null) { } // Moq requires empty constructor
        public MyDBConnection(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        #region DbCommands
        public virtual void CalculateBestCustomers(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            this.Execute("UPDATE [Person].[Person] SET [FirstName]=[FirstName]", param: null, transaction: transaction, commandTimeout: commandTimeout);
        }
        #endregion

        #region Custom Repositories
        public override IReadWriteDbRepository<TEntity> GetReadWriteRepository<TEntity>()
        {
            if (typeof(TEntity) == typeof(Person))
                return (IReadWriteDbRepository<TEntity>) new PersonRepository(this);
            return base.GetReadWriteRepository<TEntity>();
        }
        #endregion
    }
}
