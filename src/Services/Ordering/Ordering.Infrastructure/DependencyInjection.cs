﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Application.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
       public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
       {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddSingleton<AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                var auditableInterceptor = sp.GetService<AuditableEntityInterceptor>();
                options.AddInterceptors(auditableInterceptor!);
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
       }
       
    }
}