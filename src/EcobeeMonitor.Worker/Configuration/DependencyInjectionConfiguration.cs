﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EcobeeMonitor.Core.Configuration;

namespace EcobeeMonitor.Worker.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void Configure(HostBuilderContext context, IServiceCollection services)
        {
            services.ConfigureApplication();
        }
    }
}
