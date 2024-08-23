﻿using Microservice.Order.History.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microservice.Order.History.Api.Extensions;

public static class JwtExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(optiones =>
        {
            optiones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            optiones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            optiones.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = EnvironmentVariables.JwtAudience,
                ValidIssuer = EnvironmentVariables.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JwtSymmetricSecurityKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            o.MapInboundClaims = false;
        });

        services.AddAuthorization();
    }
}