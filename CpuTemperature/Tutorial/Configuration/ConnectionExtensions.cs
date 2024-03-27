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
            });
        }

    }
}
