using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.DapperFastCRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureWorks.Core.Tests.ExtensionMethods
{
    public static class PersonQueryExtensions
    {
        public static IEnumerable<Person> GetBestCustomers(this IReadDbRepository<Person> repo)
        {
            return repo.Query("SELECT TOP 100 * FROM [Person].[Person]");
        }
    }
}
