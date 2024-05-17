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
            DateTimeChanged?.Invoke(this, new(this.dateTime));
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
            LocationChanged?.Invoke(this, new(this.location));
        }
    }

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<DateTime>>? DateTimeChanged;

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<Geographic>>? LocationChanged;

    public event PlatformEventHandler<ITimeMachineManager, PlatformEventArgs<(DateTime DateTime, Geographic Location)>>? TimeMachineUpdated;

    public void Update(DateTime dateTime, Geographic location)
    {
        this.dateTime = dateTime;
        this.location = location;
        TimeMachineUpdated?.Invoke(this, new((this.dateTime, this.location)));
    }
}
