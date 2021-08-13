using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Harbin.DataAccess.DapperSimpleCRUD.Connections
{
    public static class DependencyContainer
    {
        /// <summary>
        /// For systems which have multiple database connections, registers a IReadOnlyDbConnection{DB}
        /// </summary>
        public static IServiceCollection AddReadOnlyDbConnection<DB>(this IServiceCollection services, IDbConnectionFactory connFactory)
        {
            services.AddScoped<IReadDbConnection<DB>>(sp => new ReadOnlyDbConnection<DB>(connFactory.CreateConnection()));
            return services;
        }

        /// <summary>
        /// For systems which have only single database connection, registers a IReadOnlyDbConnection{DB}
        /// </summary>
        public static IServiceCollection AddReadOnlyDbConnection(this IServiceCollection services, IDbConnectionFactory connFactory)
        {
            services.AddScoped<IReadDbConnection>(sp => new ReadOnlyDbConnection(connFactory.CreateConnection()));
            return services;
        }

        /// <summary>
        /// For systems which have multiple database connections, registers a IReadWriteDbConnection{DB}
        /// </summary>
        public static IServiceCollection AddReadWriteDbConnection<DB>(this IServiceCollection services, IDbConnectionFactory connFactory)
        {
            services.AddScoped<IReadWriteDbConnection<DB>>(sp => new ReadWriteDbConnection<DB>(connFactory.CreateConnection()));
            return services;
        }

        /// <summary>
        /// For systems which have only single database connection, registers a IReadWriteDbConnection{DB}
        /// </summary>
        public static IServiceCollection AddReadWriteDbConnection(this IServiceCollection services, IDbConnectionFactory connFactory)
        {
            services.AddScoped<IReadDbConnection>(sp => new ReadWriteDbConnection(connFactory.CreateConnection()));
            return services;
        }


        /// <summary>
        /// For systems which have only single database connection, registers IDbConnectionFactory and IDbConnection
        /// </summary>
        public static IServiceCollection AddIDbConnection(this IServiceCollection services, IDbConnectionFactory connFactory)
        {
            services.AddSingleton<IDbConnectionFactory>(connFactory);
            services.AddTransient<IDbConnection>(s => s.GetService<IDbConnectionFactory>().CreateConnection());
            return services;
        }
    }
}
