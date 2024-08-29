using System.Runtime.Versioning;
using Iot.Device.Bmxx80.PowerMode;
using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Configuration.Hardware;
using SignalF.Configuration.Hardware.Gpio;
using SignalF.Configuration.Hardware.I2c;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Hardware.DeviceBindings;
using SignalF.Controller.Signals.Devices;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;
using SignalF.Devices.CpuTemperature;
using SignalF.Devices.IotDevices.Bme280;
using SignalF.Extensions.Configuration;

namespace Tutorial.Configuration;

public static class DeviceExtensions
{
    [SupportedOSPlatform("linux")]
    public static ISignalFConfiguration AddDevices(this ISignalFConfiguration configuration)
    {
        return configuration.AddCpuTemperature("CPU", "Temperature")
                            .AddGpio()
                            .AddBme280();
    }

    private static ISignalFConfiguration AddBme280(this ISignalFConfiguration configuration)
    {
        return configuration.AddBme280Template<Bme280>(builder => { builder.SetName("Bme280Template"); })
                            .AddBme280Definition<Bme280>(builder =>
                            {
                                builder.SetName("Bme280Definition")
                                       .UseTemplate("Bme280Template")
                                       .AddSignalSourceDefinition("Temperature", EUnitType.Temperature);
                            })
                            .AddBme280Configuration(builder =>
                            {
                                builder.SetName("Bme280")
                                       .UseDefinition("Bme280Definition")
                                       .SetOptions(new Bme280Options
                                       {
                                           PowerMode = Bmx280PowerMode.Normal
                                       })
                                       .AddSignalSourceConfiguration("Temperature", Temperature.Units.DegreeCelsius);
                            })
                            .AddI2cDeviceBinding(builder =>
                            {
                                builder.SetName("I2C1")
                                       .SetOptions(new I2cDeviceBindingOptions())
                                       .SetBusId(1);
                            })
                            .AddI2cChannelGroup(builder =>
                            {
                                builder.SetName("I2C1")
                                       .SetDeviceBinding("I2C1")
                                       .SetOptions(new I2cChannelOptions())
                                       .AddI2cChannel(channelBuilder =>
                                       {
                                           channelBuilder.SetName("I2CChannel")
                                                         .SetDeviceAddress(0x76);
                                       });

                            }).AddChannelToDeviceMapping(builder =>
                            {
                                builder.AddMapping("I2C1.I2CChannel", "Bme280");
                            });
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
                                     .SetOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 17
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO18")
                                     .SetOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 18
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO22")
                                     .SetOptions(new GpioChannelOptions
                                     {
                                         DriveMode = EGpioPinDriveMode.Output,
                                         SharingMode = EGpioSharingMode.Exclusive,
                                         PinNumber = 22
                                     });
                   })
                   .AddGpioChannel(channelBuilder =>
                   {
                       channelBuilder.SetName("GPIO27")
                                     .SetOptions(new GpioChannelOptions
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
        configuration.AddGpioPinAccessDefinition<IGpioPinAccess>(builder =>
        {
            builder.SetName("GpioPinAccessDefinition")
                   .UseTemplate("DefaultTemplate")
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
