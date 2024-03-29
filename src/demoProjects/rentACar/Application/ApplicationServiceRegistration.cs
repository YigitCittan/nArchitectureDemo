﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using MediatR;
using Application.Features.Brands.Rules;
using Core.Application.Pipelines.Validation;
using Application.Features.Auths.Rules;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Application.Services.AuthService;

namespace Application;
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());


        services.AddScoped<BrandBussinesRules>();
        services.AddScoped<AuthBussinesRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddScoped<IAuthService, AuthManager>();


        return services;

    }
}
