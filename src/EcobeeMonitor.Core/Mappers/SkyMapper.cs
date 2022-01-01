namespace EcobeeMonitor.Core.Mappers
{
    public class SkyMapper
    {
        public string Map(string toMap)
        {
            //Per values found at https://www.ecobee.com/home/developer/api/documentation/v1/objects/WeatherForecast.shtml
            if (string.IsNullOrWhiteSpace(toMap)) return toMap;

            switch (toMap.Trim())
            {
                case "1": return "SUNNY";
                case "2": return "CLEAR";
                case "3": return "MOSTLY SUNNY";
                case "4": return "MOSTLY CLEAR";
                case "5": return "HAZY SUNSHINE";
                case "6": return "HAZE";
                case "7": return "PASSING CLOUDS";
                case "8": return "MORE SUN THAN CLOUDS";
                case "9": return "SCATTERED CLOUDS";
                case "10": return "PARTLY CLOUDY";
                case "11": return "A MIXTURE OF SUN AND CLOUDS";
                case "12": return "HIGH LEVEL CLOUDS";
                case "13": return "MORE CLOUDS THAN SUN";
                case "14": return "PARTLY SUNNY";
                case "15": return "BROKEN CLOUDS";
                case "16": return "MOSTLY CLOUDY";
                case "17": return "CLOUDY";
                case "18": return "OVERCAST";
                case "19": return "LOW CLOUDS";
                case "20": return "LIGHT FOG";
                case "21": return "FOG";
                case "22": return "DENSE FOG";
                case "23": return "ICE FOG";
                case "24": return "SANDSTORM";
                case "25": return "DUSTSTORM";
                case "26": return "INCREASING CLOUDINESS";
                case "27": return "DECREASING CLOUDINESS";
                case "28": return "CLEARING SKIES";
                case "29": return "BREAKS OF SUN LATE";
                case "30": return "EARLY FOG FOLLOWED BY SUNNY SKIES";
                case "31": return "AFTERNOON CLOUDS";
                case "32": return "MORNING CLOUDS";
                case "33": return "SMOKE";
                case "34": return "LOW LEVEL HAZE";

                default: return toMap;
            }
        }
    }
}
