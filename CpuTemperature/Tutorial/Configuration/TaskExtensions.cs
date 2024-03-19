using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Configuration;

namespace Tutorial.Configuration;

public static class TaskExtensions
{
    private static readonly string OneSecondWriteTask = "1s write task";
    private static readonly string OneSecondReadTask = "1s read task";

    public static ISignalFConfiguration AddTasks(this ISignalFConfiguration configuration)
    {
        configuration.AddTasks(builder =>
        {
            builder.AddTask(OneSecondWriteTask, 0, ETaskType.Write, 1.0, Time.Units.Second);
            builder.AddTask(OneSecondReadTask, 0, ETaskType.Read, 1.0, Time.Units.Second);
        });

        return configuration;
    }

    public static ISignalFConfiguration AddTaskMappings(this ISignalFConfiguration configuration)
    {
        configuration.AddTaskMappings(builder =>
        {
            builder.MapSignalProcessorToTask("CpuTemperature", OneSecondWriteTask);
        });

        return configuration;
    }
}