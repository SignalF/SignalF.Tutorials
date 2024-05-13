using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Signals;
using SignalF.Extensions.Configuration;

namespace Tutorial.Monitoring;

public static class MonitoringExtensions
{
    public static IServiceCollection AddTemperatureMonitoring(this IServiceCollection services)
    {
        return services.AddTransient<TemperatureMonitoring>();
    }

    public static ISignalFConfiguration AddTemperatureMonitoring(this ISignalFConfiguration configuration)
    {
        return configuration.AddTemperatureMonitoringDefinition()
                            .AddTemperatureMonitoringConfiguration();
    }

    private static ISignalFConfiguration AddTemperatureMonitoringDefinition(this ISignalFConfiguration configuration)
    {
        return configuration.AddCalculatorDefinition<TemperatureMonitoring>(builder =>
        {
            builder.UseTemplate("DefaultTemplate")
                   .SetName("TemperatureMonitoring")
                   .AddSignalSinkDefinition("CpuTemperature", EUnitType.Temperature)
                   .AddSignalSourceDefinition("OK")
                   .AddSignalSourceDefinition("Warning")
                   .AddSignalSourceDefinition("Alarm")
                   .AddSignalSourceDefinition("Fan");
        });
    }

    private static ISignalFConfiguration AddTemperatureMonitoringConfiguration(this ISignalFConfiguration configuration)
    {
        return configuration.AddCalculatorConfiguration<TemperatureMonitoring>(builder =>
        {
            builder.UseDefinition("TemperatureMonitoring")
                   .SetName("TemperatureMonitoring")
                   .AddSignalSinkConfiguration("CpuTemperature", "CpuTemperature", Temperature.Units.DegreeCelsius)
                   .AddSignalSourceConfiguration("OK", "OK")
                   .AddSignalSourceConfiguration("Warning", "Warning")
                   .AddSignalSourceConfiguration("Alarm", "Alarm")
                   .AddSignalSourceConfiguration("Fan", "Fan");
        });
    }
}
