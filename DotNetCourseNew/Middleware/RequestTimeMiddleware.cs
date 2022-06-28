using System.Diagnostics;

namespace DotNetCourseNew.Middleware;

public class RequestTimeMiddleware : IMiddleware
{
    private readonly ILogger<RequestTimeMiddleware> _logger;

    public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await next.Invoke(context);
        stopwatch.Stop();
        TimeSpan timeSpan = stopwatch.Elapsed;
        if (timeSpan > TimeSpan.FromSeconds(4))
            _logger.LogDebug("Time taken {TimeSpan}", timeSpan);
    }
}