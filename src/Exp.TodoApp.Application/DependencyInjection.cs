﻿

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Exp.TodoApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //// 🔹 Register all validators from the Application assembly
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //// 🔹 Register pipeline behavior for validation
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // 🔹 Add MediatR from the same Application assembly
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
         
        services.AddHttpContextAccessor();

        // 🔹 AutoMapper with current domain assemblies
        services.AddAutoMapper(config =>
        {
            config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        });

        return services;
    }

}

