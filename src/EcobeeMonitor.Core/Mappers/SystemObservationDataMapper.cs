using EcobeeMonitor.Core.Extensions;
using EcobeeMonitor.Core.Models;
using EcobeeMonitor.Core.Models.Ecobee;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcobeeMonitor.Core.Mappers
{
    public class SystemObservationDataMapper
    {
        private readonly SkyMapper _skyMapper;

        public SystemObservationDataMapper(SkyMapper skyMapper)
        {
            _skyMapper = skyMapper;
        }

        public ICollection<SystemObservationData> Map(RuntimeReportResult toMap)
        {
            var positions = ParseFieldPositions(toMap.Columns);
            var rows = toMap.ReportList.SelectMany(s => s.RowList).ToList();

            var results = rows
                .AsParallel()
                .Select(row =>
                {
                    var items = row.Split(",");
                    var data = Map(items, positions);

                    return data;
                })
                .Where(HasData)
                .ToList();

            return results;
        }

        private SystemObservationData Map(string[] parsedData, Dictionary<string, int> positions)
        {
            var date = parsedData.GetColumnValue("date", positions);
            var time = parsedData.GetColumnValue("time", positions);
            var observedAt = DateTime.Parse($"{date} {time}");

            var skyValue = parsedData.GetColumnValue(RuntimeReportColumns.sky.Name, positions);

            return new SystemObservationData
            {
                At = observedAt,

                OutdoorTemperature = parsedData.GetColumnValue(RuntimeReportColumns.outdoorTemp.Name,positions).ToDouble(),
                OutdoorHumidity = parsedData.GetColumnValue(RuntimeReportColumns.outdoorHumidity.Name,positions).ToDouble()/100,
                SkyCover = _skyMapper.Map(skyValue),

                ZoneAverageTemperature = parsedData.GetColumnValue(RuntimeReportColumns.zoneAveTemp.Name,positions).ToDouble(),
                ZoneCoolTemperature = parsedData.GetColumnValue(RuntimeReportColumns.zoneCoolTemp.Name,positions).ToDouble(),
                ZoneHeatTemperature = parsedData.GetColumnValue(RuntimeReportColumns.zoneHeatTemp.Name,positions).ToDouble(),
                ZoneHvacMode = parsedData.GetColumnValue(RuntimeReportColumns.zoneHvacMode.Name,positions),
                HvacMode = parsedData.GetColumnValue(RuntimeReportColumns.hvacMode.Name,positions),

                Stage1HeatRuntime = parsedData.GetColumnValue(RuntimeReportColumns.auxHeat1.Name,positions).ToDouble(),
                Stage2HeatRuntime = parsedData.GetColumnValue(RuntimeReportColumns.auxHeat2.Name,positions).ToDouble(),
                Stage3HeatRuntime = parsedData.GetColumnValue(RuntimeReportColumns.auxHeat3.Name,positions).ToDouble(),

                Stage1CoolRuntime = parsedData.GetColumnValue(RuntimeReportColumns.compCool1.Name,positions).ToDouble(),
                Stage2CoolRuntime = parsedData.GetColumnValue(RuntimeReportColumns.compCool2.Name,positions).ToDouble(),

                FanRuntime = parsedData.GetColumnValue(RuntimeReportColumns.fan.Name,positions).ToDouble(),
            };
        }

        private bool HasData(SystemObservationData item)
        {
            return item.Stage1HeatRuntime.HasValue || item.Stage1CoolRuntime.HasValue;
        }

        private Dictionary<string, int> ParseFieldPositions(string columnNames)
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"date",0 },
                {"time",1 },
            };

            var columns = columnNames.Split(",");
            var index = result.Max(r=>r.Value)+1;

            foreach (var item in columns)
            {
                result.TryAdd(item, index);
                index++;
            }

            return result;
        }
    }
}
