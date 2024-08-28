using SignalF.Configuration;
using SignalF.DataOutput.Console;
using Console = SignalF.DataOutput.Console.Console;

namespace Tutorial.Configuration;

public static class DataOutputExtensions
{
    public static ISignalFConfiguration AddDataOutputs(this ISignalFConfiguration configuration)
    {
        configuration.AddDataOutputConfiguration(builder =>
        {
            builder.SetName("Measurement")
                   //.AddSignalSource("CPU.Temperature")
                   .AddSignalSource("Bme280.Temperature")
                   .AddDataOutputSender("DataOutputSenderConsole");
        });

        return configuration;
    }

    public static ISignalFConfiguration AddDataOutputSenders(this ISignalFConfiguration configuration)
    {
        configuration.AddConsoleConfiguration(
            builder =>
            {
                builder.SetName("DataOutputSenderConsole")
                       .SetType<Console>()
                       .SetOptions(new ConsoleOptions
                       {
                           ShowTimestamp = true
                       });
            });

        return configuration;
    }
}
