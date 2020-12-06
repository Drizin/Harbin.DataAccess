using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connection;
using Harbin.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdventureWorks.Core.Tests.CustomClassesReadWrite
{
    public class PersonReadWriteRepository : ReadWriteDbRepository<Person>
    {
        protected PersonReadWriteRepository() : base(null) { } // Moq requires empty constructor
        public PersonReadWriteRepository(MyDBReadWriteConnection db) : base(db)
        {
        }
        public virtual void CalculateBestCustomers(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            _db.Execute("UPDATE [Person].[Person] SET [FirstName]=[FirstName]", param: null, transaction: transaction, commandTimeout: commandTimeout);
        }
    }
}
