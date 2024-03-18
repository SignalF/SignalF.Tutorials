using SignalF.Configuration;
using SignalF.Controller.Configuration;
using SignalF.Datamodel.Configuration;

namespace Tutorial;

public class MyConfiguration : ISystemConfiguration
{
    private readonly Func<ICoreConfiguration> _coreConfigurationFactory;

    public MyConfiguration(Func<ICoreConfiguration> coreConfigurationFactory)
    {
        _coreConfigurationFactory = coreConfigurationFactory;
    }

    public void Configure(IControllerConfiguration configuration)
    {
        var coreConfig = _coreConfigurationFactory();

        //coreConfig.AddTasks()
        //    .AddTaskMappings()
        coreConfig.Build(configuration);
    }
}