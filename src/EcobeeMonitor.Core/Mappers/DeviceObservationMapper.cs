using EcobeeMonitor.Core.Extensions;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EcobeeMonitor.Core.Mappers
{
    public class DeviceObservationMapper
    {
        public ICollection<DeviceObservationData> Map(RuntimeReportResult toMap,
            ICollection<DeviceData> devices)
        {
            var positions = ParseFieldPositions(toMap.SensorList);
            var data = toMap.SensorList.SelectMany(s => s.Data).ToList();

            var results = new ConcurrentDictionary<string, DeviceObservationData>();

            foreach (var row in data)
            {
                var items = row.Split(",");
                var date = items.GetColumnValue("date", positions);
                var time = items.GetColumnValue("time", positions);

                foreach (var device in devices)
                {
                    var deviceData = items.GetColumnValue(device.SensorId, positions);
                    var key = $"{device.DeviceId}|{date}|{time}";

                    var observedAt = DateTime.Parse($"{date} {time}");

                    results.AddOrUpdate(key,
                        NewDeviceObservation(device, deviceData, observedAt),
                        (key, existing) => UpdateDeviceObservation(device, deviceData, existing));

                }
            }

            var observationsWithData = results.Values
                .Where(o => o.Humidity.HasValue || o.Temperature.HasValue)
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
                Humidity = string.Equals(device.Type, SensorTypes.Humidity) ? value.ToDouble() / 100d : null,
                Temperature = string.Equals(device.Type, SensorTypes.Temperature) ? value.ToDouble() : null,
            };
        }

        private DeviceObservationData UpdateDeviceObservation(DeviceData device,
            string value,
            DeviceObservationData existing)
        {
            switch (device.Type)
            {
                case SensorTypes.Humidity: existing.Humidity = value.ToDouble() / 100d; break;
                case SensorTypes.Temperature: existing.Temperature = value.ToDouble(); break;
            }

            return existing;
        }

        private Dictionary<string, int> ParseFieldPositions(ICollection<RuntimeSensorReport> toParse)
        {
            var result = new Dictionary<string, int>();

            var columns = toParse.SelectMany(s => s.Columns);

            var index = 0;
            foreach (var item in columns)
            {
                result.TryAdd(item, index);
                index++;
            }

            return result;
        }
    }
}
