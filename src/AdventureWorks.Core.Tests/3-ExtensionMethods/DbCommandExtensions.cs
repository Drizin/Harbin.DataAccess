using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Connections;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Data;

namespace AdventureWorks.Core.Tests.ExtensionMethods
{
    public static class DbCommandExtensions
    {
        public static void CalculateBestCustomers(this IReadWriteDbConnection conn, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            conn.Execute("UPDATE [Person].[Person] SET [FirstName]=[FirstName]", param: null, transaction: transaction, commandTimeout: commandTimeout);
        }

        public static void UpdateCustomers(this IReadWriteDbRepository<Person> repo, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            repo.Execute("UPDATE [Person].[Person] SET [FirstName]=@firstName WHERE [PersonType]='EM' ", 
                param: new { firstName = "Rick" }, transaction: transaction, commandTimeout: commandTimeout);
        }
    }
}
