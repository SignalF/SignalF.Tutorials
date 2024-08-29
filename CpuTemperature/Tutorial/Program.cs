using System.Runtime.Versioning;
using SignalF.Controller.Configuration;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Hardware.DeviceBindings;
using SignalF.DataOutput.Console;
using SignalF.Devices.CpuTemperature;
using SignalF.Devices.IotDevices.Bme280;
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
        //var loop = true;
        //while (loop)
        //{
        //    Thread.Sleep(1000);
        //}
        var hostBuilder = Host.CreateDefaultBuilder(args)
                              .UseSignalFController()
                              .UseSignalFConfiguration()
                              .ConfigureServices(services =>
                              {
                                  services.AddSignalFControllerService();
                                  services.AddTransient<ISystemConfiguration, SystemConfiguration>();

                                  // Register device implementations
                                  services.AddCpuTemperature();
                                  services.AddBme280();
                                  services.AddTransient<II2cDeviceBinding, I2cDeviceBinding>();

                                  // Register calculator implementations
                                  services.AddTemperatureMonitoring();

                                  // Register data outputs.
                                  services.AddConsole();
                              });

        var host = hostBuilder.Build();

        await host.RunAsync();
    }
}
