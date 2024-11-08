namespace webapitest;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log request information
        Console.WriteLine($"Received request: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline
        await _next(context);
    }
}