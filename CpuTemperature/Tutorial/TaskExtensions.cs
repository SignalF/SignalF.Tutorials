using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Configuration;

namespace Tutorial;

public static class TaskExtensions
{
    private static readonly string OneSecondWriteTask = "1s write task";

    public static ICoreConfiguration AddTasks(this ICoreConfiguration configuration)
    {
        configuration.AddTasks(builder =>
        {
            builder.AddTask(OneSecondWriteTask, 0, ETaskType.Write, 1.0, Time.Units.Second);
        });

        return configuration;
    }

    public static ICoreConfiguration AddTaskMappings(this ICoreConfiguration configuration)
    {
        configuration.AddTaskMappings(builder =>
        {
            builder.MapSignalProcessorToTask("DefaultProcedure", OneSecondWriteTask);
        });

        return configuration;
    }
}