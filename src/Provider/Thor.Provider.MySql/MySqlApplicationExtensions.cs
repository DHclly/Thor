﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thor.Core.Extensions;

namespace Thor.Provider;

public static class MySqlApplicationExtensions
{
    public static IServiceCollection AddThorMySqlDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddThorDataAccess<MySqlThorContext>(((provider, builder) =>
        {
            builder.UseMySql(configuration.GetConnectionString("ConnectionString"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionString")));
        }));


        services.AddLocalDataAccess<MySqlLoggerContext>(((provider, builder) =>
        {
            builder.UseMySql(configuration.GetConnectionString("LoggerConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionString")));
        }));

        return services;
    }
}