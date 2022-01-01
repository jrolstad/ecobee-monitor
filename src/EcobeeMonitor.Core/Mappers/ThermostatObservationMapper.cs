using EcobeeMonitor.Core.Extensions;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using EcobeeMonitor.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EcobeeMonitor.Core.Mappers
{
    public class ThermostatObservationMapper
    {
        public ThermostatObservation Map(string thermostatId, RuntimeReportResult toMap)
        {
            var devices = MapDevices(toMap);

            return new ThermostatObservation
            {
                At = ClockService.Now,
                ThermostatId = thermostatId,
                Devices = devices,
                DeviceObservations = MapDeviceObservations(toMap, devices)
            };
        }

        private static List<DeviceData> MapDevices(RuntimeReportResult toMap)
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

        private ICollection<DeviceObservationData> MapDeviceObservations(RuntimeReportResult toMap, 
            ICollection<DeviceData> devices)
        {
            var positions = ParseFieldPositions(toMap.SensorList);
            var data = toMap.SensorList.SelectMany(s => s.Data).ToList();

            var results = new ConcurrentDictionary<string, DeviceObservationData>();

            foreach(var row in data)
            {
                var items = row.Split(",");
                var date = GetColumnValue("date", items, positions);
                var time = GetColumnValue("time", items, positions);

                foreach(var device in devices)
                {
                    var deviceData = GetColumnValue(device.SensorId, items, positions);
                    var key = $"{device.DeviceId}|{date}|{time}";

                    var observedAt = DateTime.Parse($"{date} {time}");

                    results.AddOrUpdate(key,
                        NewDeviceObservation(device, deviceData, observedAt),
                        (key, existing) => UpdateDeviceObservation(device, deviceData, observedAt, existing));

                }
            }
            
            var observationsWithData = results.Values
                .Where(o=>o.Humidity.HasValue && o.Temperature.HasValue)
                .ToList();

            return observationsWithData;
        }

        private DeviceObservationData NewDeviceObservation(DeviceData device, 
            string value,
            DateTime observedAt)
        {
            return new DeviceObservationData
            {
                DeviceId = device.DeviceId,
                At = observedAt,
                Humidity = string.Equals(device.Type, SensorTypes.Humidity) ? value.ToDouble()/100d : null,
                Temperature = string.Equals(device.Type, SensorTypes.Temperature) ? value.ToDouble() : null,
            };
        }

        private DeviceObservationData UpdateDeviceObservation(DeviceData device, 
            string value,
            DateTime observedAt,
            DeviceObservationData existing)
        {
            switch(device.Type)
            {
                case SensorTypes.Humidity:existing.Humidity = value.ToDouble()/100d; break;
                case SensorTypes.Temperature:existing.Temperature = value.ToDouble(); break;
            }

            return existing;
        }

        private Dictionary<string,int> ParseFieldPositions(ICollection<RuntimeSensorReport> toParse)
        {
            var result = new Dictionary<string,int>();
            
            var columns = toParse.SelectMany(s => s.Columns);

            var index = 0;
            foreach(var item in columns)
            {
                result.TryAdd(item, index);
                index++;
            }

            return result;
        }

        private string GetColumnValue(string field, string[] data, Dictionary<string, int> positions)
        {
            if(positions.TryGetValue(field, out var index))
            {
                return data[index];
            }
            return null;
        }


    }
}
