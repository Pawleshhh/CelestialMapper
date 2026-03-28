using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

[Export(typeof(ITimeMachineManager), typeof(TimeMachineManager), IsSingleton = true, Key = nameof(TimeMachineManager))]
public class TimeMachineManager : ITimeMachineManager
{

    #region Fields

    private readonly IDictionary<Guid, TimeMachineData> timeMachineData = new Dictionary<Guid, TimeMachineData>();

    #endregion

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<TimeMachineData>>? TimeMachineUpdated;

    public Geographic? GetLocation(Guid guid)
    {
        if (this.timeMachineData.TryGetValue(guid, out var data))
        {
            return data.Location;
        }

        return default;
    }

    public DateTime? GetTime(Guid guid)
    {
        if (this.timeMachineData.TryGetValue(guid, out var data))
        {
            return data.DateTime;
        }

        return default;
    }

    public bool HasTimeMachine(Guid guid)
    {
        return this.timeMachineData.ContainsKey(guid);
    }

    public void RemoveTimeMachine(Guid guid)
    {
        this.timeMachineData.Remove(guid);
    }

    public void Update(Guid guid, DateTime dateTime, Geographic location)
    {
        if (this.timeMachineData.TryGetValue(guid, out var data))
        {
            this.timeMachineData[guid] = data with { DateTime = dateTime, Location = location };
            return;
        }

        var timeMachineData = new TimeMachineData(dateTime, location);
        this.timeMachineData.Add(guid, timeMachineData);
        TimeMachineUpdated?.Invoke(this, new(timeMachineData));
    }
}
