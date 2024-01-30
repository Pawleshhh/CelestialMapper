﻿namespace CelestialMapper.Core.Database;

internal static class DbColumnNames
{

    public static StarsColumnNames StarsColumnNames { get; } = new(
        "stars",
        "id",
        "hip",
        "hd",
        "hr",
        "gl",
        "bf",
        "proper",
        "ra",
        "dec",
        "dist",
        "pmra",
        "pmdec",
        "rv",
        "mag",
        "absmag",
        "spect",
        "ci",
        "x",
        "y",
        "z",
        "vx",
        "vy",
        "vz",
        "rarad",
        "decrad",
        "pmrarad",
        "pmdecrad",
        "bayer",
        "flam",
        "con",
        "comp",
        "comp_primary",
        "base",
        "lum",
        "var",
        "var_min",
        "var_max");

    public static ConstellationLinesColumnNames ConstellationLinesColumnNames { get; } = new(
        "id", 
        StarsColumnNames.Constellation, 
        "line_id", 
        StarsColumnNames.HarvardYaleBrightStarCatalogId);

}

record StarsColumnNames(
    string TableName,
    string Id,
    string HipparcosId,
    string HendryDraperId,
    string HarvardYaleBrightStarCatalogId,
    string GlieseId,
    string BayerFlamsteedId,
    string CommonName,
    string RightAcension,
    string Declination,
    string Distance,
    string RaProperMotion,
    string DecProperMotion,
    string StarRadialVelocity,
    string Magnitude,
    string AbsoluteVisualMagnitude,
    string SpectralType,
    string ColorIndex,
    string XCartesianCoord,
    string YCartesianCoord,
    string ZCartesianCoord,
    string XCartesianVelocity,
    string YCartesianVelocity,
    string ZCartesianVelocity,
    string RightAscensionRad,
    string DeclinationRad,
    string RaProperMotionRad,
    string DecProperMotionRad,
    string BayerDesignation,
    string FlamsteedNumber,
    string Constellation,
    string CompanionStarId,
    string PrimaryStarId,
    string MultiStarSystemId,
    string StarLuminosity,
    string StarStandardVariable,
    string StarApproximateMinMag,
    string StarApproximateMaxMag);

record ConstellationLinesColumnNames(string Id, string ConstellationName, string LineId, string HarvardYaleBrightStarCatalogId);