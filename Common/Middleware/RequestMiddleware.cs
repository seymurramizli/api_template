using Common.Logging;
using Microsoft.IO;

namespace Common.Middleware;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly IAppLogger<RequestMiddleware> _logger;
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    public RequestMiddleware(RequestDelegate next/*, IAppLogger<RequestMiddleware> logger*/)
    {
        _next = next;
        //_logger = logger;
        _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task InvokeAsync(HttpContext httpContext, IAppLogger<RequestMiddleware> logger)
    {
        await LogRequestAsync(httpContext, logger);

        await _next(httpContext);
    }

    private async Task LogRequestAsync(HttpContext context, IAppLogger<RequestMiddleware> logger)
    {
        context.Request.EnableBuffering();
        await using var requestStream = _recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);
        logger.LogInformation($"Schema:{context.Request.Scheme} " +
                               $"Host: {context.Request.Host} " +
                               $"Path: {context.Request.Path} " +
                               $"QueryString: {context.Request.QueryString} " +
                               $"Request Body: {ReadStreamInChunks(requestStream)}");
        context.Request.Body.Position = 0;
    }

    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;
        stream.Seek(0, SeekOrigin.Begin);
        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);
        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;
        do
        {
            readChunkLength = reader.ReadBlock(readChunk,
                                               0,
                                               readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);
        return textWriter.ToString();
    }
}

