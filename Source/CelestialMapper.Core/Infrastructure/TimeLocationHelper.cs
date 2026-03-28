using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

[Export(typeof(TimeLocationHelper), IsKeyed = false, IsSingleton = true, Key = nameof(TimeLocationHelper))]
public class TimeLocationHelper
{

    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ILocationProvider locationProvider;

    public TimeLocationHelper(IDateTimeProvider dateTimeProvider, ILocationProvider locationProvider)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.locationProvider = locationProvider;
    }

    public DateTime DateTime => this.dateTimeProvider.GetDateTime();

    public Geographic Location => this.locationProvider.GetLocation();
}
