using AdventureWorks.Core.CoreDatabase;
using AdventureWorks.Core.Domain.Entities;
using Harbin.DataAccess.Repositories.DapperSimpleCRUD;
using System.Collections.Generic;
using System.Linq;

namespace AdventureWorks.Core.Application.ApplicationServices
{
    public class PersonService
    {
        #region Dependencies
        protected readonly CoreDbReadConnection _conn;
        protected readonly CoreDbReadWriteConnection _wconn;
        #endregion

        public PersonService(CoreDbReadConnection conn, CoreDbReadWriteConnection wconn)
        {
            _conn = conn;
            _wconn = wconn;
        }

        #region Methods
        public List<Person> GetAllPeople()
        {
            return _conn.GetReadRepository<Person>().QueryAll().ToList();
        }
        #endregion
    }
}
