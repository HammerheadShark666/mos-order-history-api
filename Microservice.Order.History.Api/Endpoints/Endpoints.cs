using Asp.Versioning;
using MediatR;
using Microservice.Order.History.Api.Extensions;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microservice.Order.History.Api.MediatR.GetOrderHistory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Microservice.Order.History.Api.Endpoints;

public static class Endpoints
{
    public static void ConfigureRoutes(this WebApplication app, ConfigurationManager configuration)
    {
        var orderGroup = app.MapGroup("v{version:apiVersion}/order-history").WithTags("order-history");

        orderGroup.MapGet("/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
        {
            var getOrderResponse = await mediator.Send(new GetOrderHistoryRequest(id));
            return Results.Ok(getOrderResponse);
        })
        .Produces<GetOrderHistoryResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .WithName("GetOrder")
        .WithApiVersionSet(app.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get a order history based on id.",
            Description = "Gets a order history based on its id.",
            Tags = new List<OpenApiTag> { new() { Name = "Microservice Order System - Orders History" } }
        });
    }
}