using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

public interface ITimeMachineManager
{
    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<TimeMachineData>>? TimeMachineUpdated;

    public void Update(Guid guid, DateTime dateTime, Geographic location);
    public void RemoveTimeMachine(Guid guid);
    public bool HasTimeMachine(Guid guid);

    public DateTime? GetTime(Guid guid);
    public Geographic? GetLocation(Guid guid);

}
