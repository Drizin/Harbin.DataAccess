namespace Harbin.DataAccess.Connections
{
    /// <summary>
    /// Wraps an underlying IDbConnection (but implements IDbConnection so can be used as IDbConnection),
    /// and exposes facade methods to invoke Dapper Query extensions (not Execute extensions).
    /// The generic type can be used if your application connects to multiple databases (different set of tables on each)
    /// </summary>
    public interface IReadDbConnection<DB> : IReadDbConnection
    {
    }
}
