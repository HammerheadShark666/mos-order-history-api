﻿using Microservice.Order.History.Api.Helpers.Exceptions;
using System.Text.Json;

namespace Microservice.Order.History.Api.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, e.Message); 
            await HandleExceptionAsync(context, e);
        } 
    }    

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {  
        var statusCode = GetStatusCode(exception);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode; 

        var response = new
        {
            status = statusCode,
            detail = GetMessage(exception)
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));                    
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        }; 

    private static string GetMessage(Exception exception) =>
        exception switch
        { 
            _ => exception.Message
        };   
}