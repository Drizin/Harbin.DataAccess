using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Data;

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
    }
}
