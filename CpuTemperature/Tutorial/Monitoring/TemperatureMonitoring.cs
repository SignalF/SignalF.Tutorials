using SignalF.Controller;
using SignalF.Controller.Signals;
using SignalF.Controller.Signals.Calculators;
using SignalF.Datamodel.Calculation;
using ICalculator = SignalF.Controller.Calculators.ICalculator;

namespace Tutorial.Monitoring;

public class TemperatureMonitoring : Calculator<ICalculatorConfiguration>, ICalculator
{
    private const double WarnLevel = 70.0;
    private const double CriticalLevel = 100.0;
    private const int NumberOfSignalSinks = 1;
    private const int NumberOfSignalSources = 4;

    private const int TemperatureIndex = 0;

    private const int OkIndex = 0;
    private const int WarningIndex = 1;
    private const int AlarmIndex = 2;
    private const int FanIndex = 3;
    private readonly int[] _signalSinkMapping = new int[NumberOfSignalSinks];

    private readonly int[] _signalSourceMapping = new int[NumberOfSignalSources];

    public TemperatureMonitoring(ISignalHub signalHub, ILogger<Calculator<ICalculatorConfiguration>> logger)
        : base(signalHub, logger)
    {
    }

    protected override void OnWrite()
    {
    }

    protected override void OnRead()
    {
    }

    protected override void OnCalculate()
    {
        var timestamp = SignalHub.GetTimestamp();
        var temperature = SignalSinks[TemperatureIndex].Value;

        SignalSources[OkIndex].AssignWith(temperature < WarnLevel ? 1.0 : 0.0, timestamp);
        SignalSources[WarningIndex].AssignWith(temperature is >= WarnLevel and < CriticalLevel ? 1.0 : 0.0, timestamp);
        SignalSources[AlarmIndex].AssignWith(temperature >= CriticalLevel ? 1.0 : 0.0, timestamp);
        SignalSources[FanIndex].AssignWith(temperature >= WarnLevel ? 1.0 : 0.0, timestamp);
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
                    _signalSinkMapping[TemperatureIndex] = GetSignalIndex(signalSink);
                    break;
                }
                default:
                {
                    throw new ControllerException($"Unsupported signal {signalName}");
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
                    _signalSourceMapping[OkIndex] = GetSignalIndex(signalSource);
                    break;
                }
                case "Warning":
                {
                    _signalSourceMapping[WarningIndex] = GetSignalIndex(signalSource);
                    break;
                }
                case "Alarm":
                {
                    _signalSourceMapping[AlarmIndex] = GetSignalIndex(signalSource);
                    break;
                }
                case "Fan":
                {
                    _signalSourceMapping[FanIndex] = GetSignalIndex(signalSource);
                    break;
                }
                default:
                {
                    throw new ControllerException($"Unsupported signal {signalName}");
                }
            }
        }
    }
}
