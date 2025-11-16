namespace Exp.TodoApp.GrpcApi.Extensions;

public static class MiddlewareExtension
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        // Add global exception handling middleware
        app.UseMiddleware<Middleware.GlobalExceptionMiddleware>();
        
        app.UseSwaggerMiddleware();
    }
}

