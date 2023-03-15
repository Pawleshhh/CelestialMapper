namespace CelestialMapper.Core;

public record EquatorialCoordinates(double Declination, double RightAscension)
{

    #region Methods

    public override string ToString()
    {
        return $"{Declination} DEC, {RightAscension} RA";
    }

    #endregion

}

public record HorizonCoordinates(double Altitude, double Azimuth)
{

    #region Const

    public const double MAX_ALTITUDE = 90.0;
    public const double MIN_ALTITUDE = 0.0;

    public const double MAX_AZIMUTH = 360.0;
    public const double MIN_AZIMUTH = 0.0;

    #endregion

    #region Methods

    public override string ToString()
    {
        return $"{Altitude} Alt, {Azimuth} Az";
    }

    #endregion

}

public record GeographicCoordinates(double Latitude, double Longitude);

public record CartesianCoordinates(double X, double Y);