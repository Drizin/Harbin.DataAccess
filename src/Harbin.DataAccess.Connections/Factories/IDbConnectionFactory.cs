using System.Data;

namespace Harbin.DataAccess.Connections
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
