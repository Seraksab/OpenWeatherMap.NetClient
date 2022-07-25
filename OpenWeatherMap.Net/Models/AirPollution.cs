using OpenWeatherMap.Net.Enums;
using UnitsNet;

namespace OpenWeatherMap.Net.Models;

public sealed class AirPollution
{
  /// <summary>
  /// Date and time, UTC
  /// </summary>
  public DateTime TimeStamp { get; set; }

  /// <summary>
  /// Air Quality Index
  /// </summary>
  public AirQualityIndex AirQualityIndex { get; internal set; }

  /// <summary>
  /// Concentration of CO (Carbon monoxide)
  /// </summary>
  public MassConcentration CarbonMonoxide { get; internal set; }

  /// <summary>
  /// Concentration of NO (Nitrogen monoxide)
  /// </summary>
  public MassConcentration NitrogenMonoxide { get; internal set; }

  /// <summary>
  /// Concentration of NO2 (Nitrogen dioxide)
  /// </summary>
  public MassConcentration NitrogenDioxide { get; internal set; }

  /// <summary>
  /// Concentration of O3 (Ozone)
  /// </summary>
  public MassConcentration Ozone { get; internal set; }

  /// <summary>
  /// Concentration of SO2 (Sulphur dioxide)
  /// </summary>
  public MassConcentration SulphurDioxide { get; internal set; }

  /// <summary>
  /// Concentration of PM2.5 (Fine particles matter)
  /// </summary>
  public MassConcentration FineParticlesMatter { get; internal set; }

  /// <summary>
  /// Concentration of PM10 (Coarse particulate matter)
  /// </summary>
  public MassConcentration CoarseParticulateMatter { get; internal set; }

  /// <summary>
  /// Concentration of NH3 (Ammonia)
  /// </summary>
  public MassConcentration Ammonia { get; internal set; }
}