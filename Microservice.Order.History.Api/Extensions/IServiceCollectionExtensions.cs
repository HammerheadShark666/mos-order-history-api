using Asp.Versioning;
using MediatR;
using Microservice.Order.History.Api.Data.Contexts;
using Microservice.Order.History.Api.Data.Repository;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Helpers;
using Microservice.Order.History.Api.Helpers.Automapper;
using Microservice.Order.History.Api.Helpers.Interfaces;
using Microservice.Order.History.Api.Helpers.Swagger;
using Microservice.Order.History.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Microservice.Order.History.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static void ConfigureExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureJwt(this IServiceCollection services)
    { 
        services.AddJwtAuthentication();
    }

    public static void ConfigureDI(this IServiceCollection services)
    { 
        services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddSingleton<ICustomerHttpAccessor, CustomerHttpAccessor>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
    }

    public static void ConfigureDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContextFactory<OrderHistoryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));
    } 

    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
            options.SupportNonNullableReferenceTypes();
        });
    }

    public static void ConfigureMediatr(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssemblyContaining<AddOrderValidator>();
        services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }

}