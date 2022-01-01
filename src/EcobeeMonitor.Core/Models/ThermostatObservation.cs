using System;
using System.Collections.Generic;

namespace EcobeeMonitor.Core.Models
{
    public class ThermostatObservation
    {
        public string ThermostatId { get; set; }
        public DateTime At { get; set; }
        public ICollection<DeviceData> Devices { get; set; }
        public ICollection<DeviceObservationData> DeviceObservations { get; set; }
        public ICollection<SystemObservationData> SystemObsevations { get; set; }
    }
    public class DeviceData
    {
        public string DeviceId { get; set; }
        public string SensorId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public static class SensorTypes
    {
        public const string Humidity = "humidity";
        public const string Temperature = "temperature";
    }

    public class DeviceObservationData
    {
        public string DeviceId { get; set; }
        public DateTime At { get; set; }

        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
    }

    public class SystemObservationData
    {
        public DateTime At { get; set; }
        public double? OutdoorTemperature { get; set; }
        public double? SkyCover { get; set; }

        public string ZoneHvacMode { get; set; }
        public double? ZoneAverageTemperature { get; set; }
        public double? ZoneHeatTemperature { get; set; }
        public double? ZoneCoolTemperature { get; set; }

        public double? Stage1HeatRuntime { get; set; }
        public double? Stage2HeatRuntime { get; set; }
        public double? Stage3HeatRuntime { get; set; }

        public double? Stage1CoolRuntime { get; set; }
        public double? Stage2CoolRuntime { get; set; }

        public double? FanRuntime { get; set; }
    }

    

}
