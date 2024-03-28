using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

public interface ITimeMachineManager
{

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, DateTime>>? DateTimeChanged;

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, Geographic>>? LocationChanged;

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, (DateTime DateTime, Geographic Location)>>? TimeMachineUpdated;

    public DateTime DateTime { get; set; }

    public Geographic Location { get; set; }

    public void Update(DateTime dateTime, Geographic location);

}
