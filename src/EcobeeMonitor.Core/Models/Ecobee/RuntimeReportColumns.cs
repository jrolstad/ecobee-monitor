namespace EcobeeMonitor.Core.Models.Ecobee
{
    public class RuntimeReportColumn
    {
        public string Name { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
    }

    public static class RuntimeReportColumns
    {
        public static RuntimeReportColumn auxHeat1 = new RuntimeReportColumn { Name = "auxHeat1", Units = "Seconds", Description = "The auxiliary heat runtime." };
        public static RuntimeReportColumn auxHeat2 = new RuntimeReportColumn { Name = "auxHeat2", Units = "Seconds", Description = "The auxiliary 2nd stage heat runtime." };
        public static RuntimeReportColumn auxHeat3 = new RuntimeReportColumn { Name = "auxHeat3", Units = "Seconds", Description = "The auxiliary 3rd stage heat runtime." };
        public static RuntimeReportColumn compCool1 = new RuntimeReportColumn { Name = "compCool1", Units = "Seconds", Description = "The compressor cool runtime." };
        public static RuntimeReportColumn compCool2 = new RuntimeReportColumn { Name = "compCool2", Units = "Seconds", Description = "The compressor 2nd stage cool runtime." };
        public static RuntimeReportColumn compHeat1 = new RuntimeReportColumn { Name = "compHeat1", Units = "Seconds", Description = "The compressor heat runtime." };
        public static RuntimeReportColumn compHeat2 = new RuntimeReportColumn { Name = "compHeat2", Units = "Seconds", Description = "The compressor 2nd stage heat runtime." };
        public static RuntimeReportColumn dehumidifier = new RuntimeReportColumn { Name = "dehumidifier", Units = "Seconds", Description = "The dehumidifier runtime." };
        public static RuntimeReportColumn dmOffset = new RuntimeReportColumn { Name = "dmOffset", Units = "Degrees F", Description = "The Demand Management temperature adjustment value the thermostat applied to the desired temperature." };
        public static RuntimeReportColumn economizer = new RuntimeReportColumn { Name = "economizer", Units = "Seconds", Description = "The economizer runtime." };
        public static RuntimeReportColumn fan = new RuntimeReportColumn { Name = "fan", Units = "Seconds", Description = "The fan runtime." };
        public static RuntimeReportColumn humidifier = new RuntimeReportColumn { Name = "humidifier", Units = "Seconds", Description = "The humidifier runtime." };
        public static RuntimeReportColumn hvacMode = new RuntimeReportColumn { Name = "hvacMode", Units = "Hvac Mode", Description = "The Mode the system was in. Values: auto, auxHeatOnly, cool, heat, off." };
        public static RuntimeReportColumn outdoorHumidity = new RuntimeReportColumn { Name = "outdoorHumidity", Units = "%", Description = "The outdoor humidity." };
        public static RuntimeReportColumn outdoorTemp = new RuntimeReportColumn { Name = "outdoorTemp", Units = "Degrees F", Description = "The outdoor temperature." };
        public static RuntimeReportColumn sky = new RuntimeReportColumn { Name = "sky", Units = "Integer", Description = "The sky cover." };
        public static RuntimeReportColumn ventilator = new RuntimeReportColumn { Name = "ventilator", Units = "Seconds", Description = "The ventilator runtime." };
        public static RuntimeReportColumn wind = new RuntimeReportColumn { Name = "wind", Units = "km/h", Description = "The wind speed." };
        public static RuntimeReportColumn zoneAveTemp = new RuntimeReportColumn { Name = "zoneAveTemp", Units = "Degrees F", Description = "The recorded average temperature." };
        public static RuntimeReportColumn zoneCalendarEvent = new RuntimeReportColumn { Name = "zoneCalendarEvent", Units = "Name of Event", Description = "The name of an event if one was running. Empty otherwise." };
        public static RuntimeReportColumn zoneClimate = new RuntimeReportColumn { Name = "zoneClimate", Units = "Name of Climate", Description = "The name of the running climate." };
        public static RuntimeReportColumn zoneCoolTemp = new RuntimeReportColumn { Name = "zoneCoolTemp", Units = "Degrees F", Description = "The set cool temperature." };
        public static RuntimeReportColumn zoneHeatTemp = new RuntimeReportColumn { Name = "zoneHeatTemp", Units = "Degrees F", Description = "The set heat temperature." };
        public static RuntimeReportColumn zoneHumidity = new RuntimeReportColumn { Name = "zoneHumidity", Units = "%", Description = "The average humidity." };
        public static RuntimeReportColumn zoneHumidityHigh = new RuntimeReportColumn { Name = "zoneHumidityHigh", Units = "%", Description = "The high humidity." };
        public static RuntimeReportColumn zoneHumidityLow = new RuntimeReportColumn { Name = "zoneHumidityLow", Units = "%", Description = "The low humidity." };
        public static RuntimeReportColumn zoneHvacMode = new RuntimeReportColumn { Name = "zoneHvacMode", Units = "System Mode", Description = "*The mode the system was in. Values: heatStage10n, heatStage20n, heatStage30n, heatOff, compressorCoolStage10n, compressorCoolStage20n, compressorCoolOff, compressorHeatStage10n, compressorHeatStage20n, compressorHeatOff, economyCycle." };
        public static RuntimeReportColumn zoneOccupancy = new RuntimeReportColumn { Name = "zoneOccupancy", Units = "", Description = "" };

    }
}
