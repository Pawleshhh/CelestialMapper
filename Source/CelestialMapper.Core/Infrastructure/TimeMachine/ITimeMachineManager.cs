using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

public interface ITimeMachineManager
{

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<DateTime>>? DateTimeChanged;

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<Geographic>>? LocationChanged;

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<(DateTime DateTime, Geographic Location)>>? TimeMachineUpdated;

    public DateTime DateTime { get; set; }

    public Geographic Location { get; set; }

    public void Update(DateTime dateTime, Geographic location);

}
