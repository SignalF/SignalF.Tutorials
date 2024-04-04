using SignalF.Controller;
using SignalF.Controller.Signals;
using SignalF.Controller.Signals.Calculators;
using SignalF.Datamodel.Calculation;

namespace Tutorial.Monitoring;

public class TemperatureMonitoring : Calculator<ICalculatorConfiguration>
{
    private const double WarnLevel = 70.0;
    private const double CriticalLevel = 100.0;

    private int _indexTemperature = -1;
    private int _indexOk = -1;
    private int _indexWarning = -1;
    private int _indexAlarm = -1;
    private int _indexFan = -1;

    public TemperatureMonitoring(ISignalHub signalHub, ILogger<TemperatureMonitoring> logger)
        : base(signalHub, logger)
    {
    }

    protected override void OnCalculate()
    {
        var timestamp = SignalHub.GetTimestamp();
        var temperature = SignalSinks[_indexTemperature].Value;

        var signalSources = SignalSources;
        signalSources[_indexOk].AssignWith(temperature < WarnLevel ? 1.0 : 0.0, timestamp);
        signalSources[_indexWarning].AssignWith(temperature is >= WarnLevel and < CriticalLevel ? 1.0 : 0.0, timestamp);
        signalSources[_indexAlarm].AssignWith(temperature >= CriticalLevel ? 1.0 : 0.0, timestamp);
        signalSources[_indexFan].AssignWith(temperature >= WarnLevel ? 1.0 : 0.0, timestamp);
    }

    protected override void OnConfigure(ICalculatorConfiguration configuration)
    {
        base.OnConfigure(configuration);

        foreach (var signalSink in configuration.SignalSinks)
        {
            var signalName = signalSink.Definition.Name;
            switch (signalName)
            {
                case "CpuTemperature":
                {
                    _indexTemperature = GetSignalIndex(signalSink);
                    break;
                }
                default:
                {
                    throw new ControllerException($"Unknown signal {signalName}");
                }
            }
        }

        foreach (var signalSource in configuration.SignalSources)
        {
            var signalName = signalSource.Definition.Name;
            switch (signalName)
            {
                case "OK":
                {
                    _indexOk = GetSignalIndex(signalSource);
                    break;
                }
                case "Warning":
                {
                    _indexWarning = GetSignalIndex(signalSource);
                    break;
                }
                case "Alarm":
                {
                    _indexAlarm = GetSignalIndex(signalSource);
                    break;
                }
                case "Fan":
                {
                    _indexFan = GetSignalIndex(signalSource);
                    break;
                }
                default:
                {
                    throw new ControllerException($"Unknown signal {signalName}");
                }
            }
        }
    }
}
