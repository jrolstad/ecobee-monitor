using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcobeeMonitor.Core.Mappers
{
    public class DeviceDataMapper
    {
        public List<DeviceData> Map(RuntimeReportResult toMap)
        {
            return toMap.SensorList
                .SelectMany(s => s.Sensors)
                .GroupBy(s => s.SensorId)
                .Select(s => new DeviceData
                {
                    DeviceId = ParseDeviceId(s.Key),
                    SensorId = s.Key,
                    Name = s.First().SensorName,
                    Type = s.First().SensorType
                })
                .ToList();
        }

        private static string ParseDeviceId(string sensorId)
        {
            var parts = sensorId.Split(":").ToList();
            parts.RemoveAt(parts.Count - 1);

            return string.Join(":", parts);
        }
    }
}
