using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ProjetoIntegradorBack.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            logger.LogInformation("Requisição cancelada pelo cliente. Método: {Method}, Rota: {Path}",
                context.Request.Method, context.Request.Path);
        }
        catch (Exception exception)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            var (statusCode, title) = exception switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
                InvalidOperationException => (StatusCodes.Status400BadRequest, "Não foi possível concluir a operação"),
                _ => (StatusCodes.Status500InternalServerError, "Erro interno do servidor")
            };

            logger.LogError(exception,
                "Erro na requisição. TraceId: {TraceId}, Método: {Method}, Rota: {Path}, " +
                "Status: {StatusCode}, Usuário: {User}, IP: {RemoteIp}",
                traceId,
                context.Request.Method,
                context.Request.Path,
                statusCode,
                context.User.Identity?.Name ?? "anônimo",
                context.Connection.RemoteIpAddress?.ToString() ?? "desconhecido");

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                type = $"https://httpstatuses.com/{statusCode}",
                title,
                status = statusCode,
                detail = exception.Message,
                traceId
            }));
        }
    }
}
