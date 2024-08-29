﻿using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Configuration;

namespace Tutorial.Configuration;

public static class TaskExtensions
{
    private const string OneSecondWriteTask = "1s write task";
    private const string OneSecondReadTask = "1s read task";
    private const string OneSecondCalculateTask = "1s calculate task";
    private const string InitTask = "init task";

    public static ISignalFConfiguration AddTasks(this ISignalFConfiguration configuration)
    {
        configuration.AddTasks(builder =>
        {
            builder.AddTask(OneSecondWriteTask, 0, ETaskType.Write, 1.0, Time.Units.Second);
            builder.AddTask(OneSecondWriteTask, 0, ETaskType.Write, 1.0, Time.Units.Second);
            builder.AddTask(OneSecondReadTask, 0, ETaskType.Read, 1.0, Time.Units.Second);
            builder.AddTask(OneSecondCalculateTask, 0, ETaskType.Calculate, 1.0, Time.Units.Second);
            builder.AddTask(InitTask, 0, ETaskType.Init, 0.0, Time.Units.Second);
        });

        return configuration;
    }

    public static ISignalFConfiguration AddTaskMappings(this ISignalFConfiguration configuration)
    {
        configuration.AddTaskMappings(builder =>
        {
            builder.MapSignalProcessorToTask("CPU", OneSecondWriteTask)
                   .MapSignalProcessorToTask("TemperatureMonitoring", OneSecondCalculateTask)
                   .MapSignalProcessorToTask("GpioPinAccess", OneSecondReadTask)
                   .MapSignalProcessorToTask("Bme280", InitTask)
                   .MapSignalProcessorToTask("Bme280", OneSecondWriteTask);
        });

        return configuration;
    }
}
