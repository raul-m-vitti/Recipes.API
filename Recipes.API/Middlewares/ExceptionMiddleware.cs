using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Recipes.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _environment = environment;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex}");
            //if(_environment.IsDevelopment()) throw;
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex switch
        {
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            FormatException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        var response = new ProblemDetails()
        {
            Detail = _environment.IsDevelopment() ? ex.Message + ex.StackTrace : null,
            Status = context.Response.StatusCode,
            Instance = context.Request.Path,
            Title = ex switch
            {
                InvalidOperationException => $"Ocorreu um erro durante a operação: {ex.Message}",
                FormatException => $"Ops! Dado no formato incorreto ou ausente: {ex.Message}",
                KeyNotFoundException => "Registro não encontrado.",
                _ => "Desculpe, encontramos um problema interno no nosso servidor. Por favor, tente novamente mais tarde ou entre em contato com nossa equipe de suporte se o problema persistir. Agradecemos sua compreensão e paciência."
            }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}