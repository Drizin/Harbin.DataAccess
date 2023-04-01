using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Data;

namespace AdventureWorks.Core.Tests.CustomClasses
{
    public class FakeMyDBConnection : MyDBConnection
    {
        public FakeMyDBConnection() : base(null)
        {
        }

        #region DbCommands
        public override void CalculateBestCustomers(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return; // do nothing! (we don't even have a real underlying IDbConnection)
        }
        #endregion
    }
}
