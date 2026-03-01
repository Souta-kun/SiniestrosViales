using SiniestrosViales.Application.Common.Models;

namespace SiniestrosViales.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Captura excepciones no manejadas y devuelve una respuesta JSON con el mensaje de error adecuado.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is ArgumentException argEx)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(
                    ApiResult.FailureResult(argEx.Message)
                );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    ApiResult.FailureResult("Internal Server Error. Por favor contacte al administrador.")
                );
            }
        }
    }
}
