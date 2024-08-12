using SignalF.Configuration;
using SignalF.DataOutput.Console;

namespace Tutorial.Configuration
{
    public static class DataOutputExtensions
    {
        public static ISignalFConfiguration AddDataOutputs(this ISignalFConfiguration configuration)
        {
            configuration.AddDataOutputConfiguration(builder =>
            {
                builder.SetName("Measurement")
                    .AddSignalSource("CPU.Temperature")
                    .AddDataOutputSender("DataOutputSenderConsole");
            });

            return configuration;
        }

        public static ISignalFConfiguration AddDataOutputSenders(this ISignalFConfiguration configuration)
        {
            configuration.AddDataOutputSenderConsole(
                builder =>
                {
                    builder.SetName("DataOutputSenderConsole")
                        .SetOptions(new ConsoleDataOutputSenderOptions
                        {
                            ShowTimestamp = true,
                        });
                });

            return configuration;
        }
    }
}
