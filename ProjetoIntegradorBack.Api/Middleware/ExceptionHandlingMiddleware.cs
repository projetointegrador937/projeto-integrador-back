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

            logger.LogError(exception,
                "Erro não tratado. TraceId: {TraceId}, Método: {Method}, Rota: {Path}, " +
                "Status esperado: {StatusCode}, Usuário: {User}, IP: {RemoteIp}",
                traceId,
                context.Request.Method,
                context.Request.Path,
                (int)HttpStatusCode.InternalServerError,
                context.User.Identity?.Name ?? "anônimo",
                context.Connection.RemoteIpAddress?.ToString() ?? "desconhecido");

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                type = "https://httpstatuses.com/500",
                title = "Erro interno do servidor",
                status = StatusCodes.Status500InternalServerError,
                detail = "Ocorreu um erro inesperado. Use o traceId para localizar os detalhes nos logs.",
                traceId
            }));
        }
    }
}
