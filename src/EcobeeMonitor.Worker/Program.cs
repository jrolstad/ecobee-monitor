using EcobeeMonitor.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EcobeeMonitor.Worker
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(DependencyInjectionConfiguration.Configure)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddEnvironmentVariables();
                })
                .Build();

            host.Run();
        }
    }
}