using System.Runtime.Versioning;
using SignalF.Controller;
using SignalF.Controller.Configuration;
using SignalF.Devices.CpuTemperature;
using SignalF.Extensions.Configuration;
using SignalF.Extensions.Controller;
using Tutorial.Configuration;

namespace Tutorial;

[SupportedOSPlatform("linux")]
public class Program
{
    public static async Task Main(string[] args)
    {
        var options = CliOptionParser.ParseCliArguments(new ApplicationArgumentCollection());
        if (options.ShowHelp)
        {
            return;
        }

        var hostBuilder = Host.CreateDefaultBuilder(args)
            .UseSignalFController()
            .UseSignalFConfiguration()
            .ConfigureServices(services =>
            {
                services.AddSignalFControllerService();
                services.AddTransient<ISystemConfiguration, SystemConfiguration>();

                // Register device implementations
                services.AddCpuTemperature();
            });

        var host = hostBuilder.Build();

        await host.RunAsync();
    }
}