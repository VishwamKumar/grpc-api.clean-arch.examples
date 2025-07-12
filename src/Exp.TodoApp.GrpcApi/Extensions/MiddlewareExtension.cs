namespace Exp.TodoApp.GrpcApi.Extensions;

public static class MiddlewareExtension
{
    public static void ConfigureMiddleware(this WebApplication app)
    {      
        app.UseSwaggerMiddleware();
    }
}
