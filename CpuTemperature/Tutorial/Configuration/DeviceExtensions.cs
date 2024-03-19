using System.Runtime.Versioning;
using SignalF.Configuration;
using SignalF.Devices.CpuTemperature;

namespace Tutorial.Configuration;

public static class DeviceExtensions
{
    [SupportedOSPlatform("linux")]
    public static ISignalFConfiguration AddDevices(this ISignalFConfiguration configuration)
    {
        configuration.AddCpuTemperature("CpuTemperature", "CPU-Temperature");

        return configuration;
    }
}