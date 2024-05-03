using SignalF.Configuration;

namespace Tutorial.Configuration
{
    public static class ConnectionExtensions
    {
        public static ISignalFConfiguration AddConnections(this ISignalFConfiguration configuration)
        {
            return configuration.AddConnections(builder =>
            {
                builder.AddConnection("CPU.Temperature", "TemperatureMonitoring.CpuTemperature");

                builder.AddConnection("TemperatureMonitoring.OK", "GpioPinAccess.OK");
                builder.AddConnection("TemperatureMonitoring.Warning", "GpioPinAccess.Warning");
                builder.AddConnection("TemperatureMonitoring.Alarm", "GpioPinAccess.Alarm");
                builder.AddConnection("TemperatureMonitoring.Fan", "GpioPinAccess.Fan");
            });
        }

    }
}
