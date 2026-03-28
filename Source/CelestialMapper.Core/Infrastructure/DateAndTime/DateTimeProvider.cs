using CelestialMapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialMapper.Core;

[Export(typeof(IDateTimeProvider), typeof(DateTimeProvider), IsKeyed = false, IsSingleton = true, Key = nameof(DateTimeProvider))]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetDateTime() => DateTime.Now;
}
