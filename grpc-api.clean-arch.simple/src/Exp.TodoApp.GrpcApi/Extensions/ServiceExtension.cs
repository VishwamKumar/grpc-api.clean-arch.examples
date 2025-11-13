namespace Exp.TodoApp.GrpcApi.Extensions;

public static class ServiceExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpc().AddJsonTranscoding();
        builder.Services.AddGrpcReflection();
        builder.Services.AddHealthChecks();
        builder.Services.AddMemoryCache();

        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        });

        builder.Services.AddApplicationServices()
                        .AddInfrastructureServices(builder.Configuration);

        builder.Services.AddGrpc();
    }
}

