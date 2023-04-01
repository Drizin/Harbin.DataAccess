using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Data;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class PersonReadWriteRepository : ReadWriteDbRepository<Person>
    {
        protected PersonReadWriteRepository() : base(null) { } // Moq requires empty constructor
        public PersonReadWriteRepository(IReadWriteDbConnection db) : base(db)
        {
        }
        public virtual void CalculateBestCustomers(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            _db.Execute("UPDATE [Person].[Person] SET [FirstName]=[FirstName]", param: null, transaction: transaction, commandTimeout: commandTimeout);
        }
    }
}
