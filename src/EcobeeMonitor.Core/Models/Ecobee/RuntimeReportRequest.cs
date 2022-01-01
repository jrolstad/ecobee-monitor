using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EcobeeMonitor.Core.Models.Ecobee
{
    public class RuntimeReportRequest
    {
        [JsonProperty("selection")]
        public RuntimeReportSelection Selection { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("startInterval")]
        public int? StartInterval { get; set; }
        [JsonProperty("endInterval")]
        public int? EndInterval { get; set; }
        [JsonProperty("columns")]
        public string Columns { get; set; }
        [JsonProperty("includeSensors")]
        public bool IncludeSensors { get; set; }
    }

    public class RuntimeReportSelection
    {
        [JsonProperty("selectionType")]
        public string SelectionType { get; set; }
        [JsonProperty("selectionMatch")]
        public string SelectionMatch { get; set; }
    }

    
}
