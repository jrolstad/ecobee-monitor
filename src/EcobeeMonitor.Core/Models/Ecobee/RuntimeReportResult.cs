using System;
using System.Collections.Generic;

namespace EcobeeMonitor.Core.Models.Ecobee
{
    public class RuntimeReportResult
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StartInterval { get; set; }
        public int? EndInterval { get; set; }
        public List<string> Columns { get; set; }
        public List<RuntimeReport> ReportList { get; set; }
        public List<RuntimeSensorReport> SensorList { get; set; }
    }

    public class RuntimeReport
    {
        public string ThermostatIdentifier { get; set; }
        public int? RowCount { get; set; }
        public List<string> RowList { get; set; }
    }

    public class RuntimeSensorReport
    {
        public string ThermostatIdentifier { get; set; }
        public List<RuntimeSensorMetadata> Sensors { get; set; }
        public List<string> Columns { get; set; }
        public List<string> Data { get; set; }
    }

    public class RuntimeSensorMetadata
    {
        public string SensorId { get; set; }
        public string SensorName { get; set; }
        public string SensorType { get; set; }
        public string SensorUsage { get; set; }
    }
}
