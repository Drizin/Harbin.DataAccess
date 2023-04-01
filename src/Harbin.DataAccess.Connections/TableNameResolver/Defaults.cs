namespace Harbin.DataAccess.Connections
{
    /// <summary>Defaults</summary>
    public class Defaults
    {
        /// <summary>
        /// Default Table Name Resolver
        /// </summary>
        public static TableNameResolver DefaultTableNameResolver { get; set; } = new TableNameResolver();
    }
}
