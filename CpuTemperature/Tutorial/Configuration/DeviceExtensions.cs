using System.Runtime.Versioning;
using SignalF.Configuration;
using SignalF.Controller.Signals.Devices;
using SignalF.Datamodel.Hardware;
using SignalF.Devices.CpuTemperature;
using SignalF.Extensions.Configuration;

namespace Tutorial.Configuration;

public static class DeviceExtensions
{
    [SupportedOSPlatform("linux")]
    public static ISignalFConfiguration AddDevices(this ISignalFConfiguration configuration)
    {
        return configuration.AddCpuTemperature("CPU", "Temperature")
                            .AddGpio();
    }

    private static ISignalFConfiguration AddGpio(this ISignalFConfiguration configuration)
    {
        return configuration.AddGpioDeviceBinding(builder => { builder.SetName("GPIOController"); })
                            .AddGpioChannelGroup()
                            .AddGpioPinAccess();
    }
    
    private static ISignalFConfiguration AddGpioChannelGroup(this ISignalFConfiguration configuration)
    {
        configuration.AddGpioChannelGroup(builder =>
        {
            builder.SetName("GPIO")
                   .SetDeviceBinding("GPIOController")
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO17")
                                     .SetChannelOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 17
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO18")
                                     .SetChannelOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 18
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO22")
                                     .SetChannelOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 22
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO27")
                                     .SetChannelOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 27
                                     });
                   });
        });


        return configuration;
    }
    
    
    private static ISignalFConfiguration AddGpioPinAccess(this ISignalFConfiguration configuration)
    {
        configuration.AddGpioPinAccessTemplate<IGpioPinAccess>(builder =>
        {
            builder.SetName("GpioPinAccessTemplate");
        });

        configuration.AddGpioPinAccessDefinition<IGpioPinAccess>(builder =>
        {
            builder.SetName("GpioPinAccessDefinition")
                   //.UseTemplate("DefaultTemplate")
                   .UseTemplate("GpioPinAccessTemplate")
                   .AddSignalSinkDefinition("OK")
                   .AddSignalSinkDefinition("Warning")
                   .AddSignalSinkDefinition("Alarm")
                   .AddSignalSinkDefinition("Fan");
        });

        configuration.AddGpioPinAccessConfiguration(builder =>
        {
            builder.SetName("GpioPinAccess")
                   .UseDefinition("GpioPinAccessDefinition")
                   .AddSignalSinkConfiguration("OK", "OK")
                   .AddSignalSinkConfiguration("Warning", "Warning")
                   .AddSignalSinkConfiguration("Alarm", "Alarm")
                   .AddSignalSinkConfiguration("Fan", "Fan")
                   .AddSignalToChannelMapping("OK", "GPIO.GPIO17")
                   .AddSignalToChannelMapping("Warning", "GPIO.GPIO22")
                   .AddSignalToChannelMapping("Alarm", "GPIO.GPIO27")
                   .AddSignalToChannelMapping("Fan", "GPIO.GPIO18");
        });

        return configuration;
    }
}