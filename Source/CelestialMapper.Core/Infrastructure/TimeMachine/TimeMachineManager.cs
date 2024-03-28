using CelestialMapper.Common;
using CelestialMapper.Core.Infrastructure.Map;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

[Export(typeof(ITimeMachineManager), typeof(TimeMachineManager), IsSingleton = true, Key = nameof(TimeMachineManager))]
public class TimeMachineManager : ITimeMachineManager
{

    #region Fields

    private DateTime dateTime;

    private Geographic location = MapConstants.DefaultLocation;

    #endregion

    public DateTime DateTime
    {
        get => this.dateTime;
        set
        {
            if (this.dateTime == value)
            {
                return;
            }

            this.dateTime = value;
            DateTimeChanged?.Invoke(new(this, this.dateTime));
        }
    }

    public Geographic Location
    {
        get => this.location;
        set
        {
            if (this.location == value)
            {
                return;
            }

            this.location = value;
            LocationChanged?.Invoke(new(this, this.location));
        }
    }

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, DateTime>>? DateTimeChanged;

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, Geographic>>? LocationChanged;

    public event PlatformEventHandler<PlatformEventArgs<ITimeMachineManager, (DateTime DateTime, Geographic Location)>>? TimeMachineUpdated;

    public void Update(DateTime dateTime, Geographic location)
    {
        this.dateTime = dateTime;
        this.location = location;
        TimeMachineUpdated?.Invoke(new(this, new(this.dateTime, this.location)));
    }
}
