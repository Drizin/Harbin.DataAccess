namespace Harbin.DataAccess.Repositories.DapperFastCRUD
{
    /// <summary>Default Factories</summary>
    public class DefaultFactories
    {
        /// <summary></summary>
        public static IReadWriteDbRepositoryFactory ReadWriteDbRepositoryFactory = new ReadWriteDbRepositoryFactory();
        
        /// <summary></summary>
        public static IReadDbRepositoryFactory ReadDbRepositoryFactory = new ReadDbRepositoryFactory();

        /// <summary></summary>
        public static IReadOnlyDbRepositoryFactory ReadOnlyDbRepositoryFactory = new ReadOnlyDbRepositoryFactory();
    }

    /// <summary>Default Factories</summary>
    public class DefaultFactories<DB>
    {
        /// <summary></summary>
        public static IReadWriteDbRepositoryFactory<DB> ReadWriteDbRepositoryFactory = new ReadWriteDbRepositoryFactory<DB>();

        /// <summary></summary>
        public static IReadDbRepositoryFactory<DB> ReadDbRepositoryFactory = new ReadDbRepositoryFactory<DB>();

        /// <summary></summary>
        public static IReadOnlyDbRepositoryFactory<DB> ReadOnlyDbRepositoryFactory = new ReadOnlyDbRepositoryFactory<DB>();
    }
}
