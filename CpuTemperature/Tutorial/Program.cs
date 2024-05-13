using System.Runtime.Versioning;
using SignalF.Controller.Configuration;
using SignalF.DataOutput.Console;
using SignalF.Devices.CpuTemperature;
using SignalF.Extensions.Configuration;
using SignalF.Extensions.Controller;
using Tutorial.Configuration;
using Tutorial.Monitoring;

namespace Tutorial;

[SupportedOSPlatform("linux")]
public class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
                              .UseSignalFController()
                              .UseSignalFConfiguration()
                              .ConfigureServices(services =>
                              {
                                  services.AddSignalFControllerService();
                                  services.AddTransient<ISystemConfiguration, SystemConfiguration>();

                                  // Register device implementations
                                  services.AddCpuTemperature();
                                  
                                  // Register calculator implementations
                                  services.AddTemperatureMonitoring();

                                  // Register data outputs.
                                  services.AddDataOutputSenderConsole();
                              });

        var host = hostBuilder.Build();

        await host.RunAsync();
    }
}
