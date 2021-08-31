using System;

namespace Lesson1WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public void CopyData(WeatherForecast weatherForecast)
        {
            Date = weatherForecast.Date;
            TemperatureC = weatherForecast.TemperatureC;
        }

        public override bool Equals(object obj) =>
            obj is WeatherForecast weatherForecast ? this == weatherForecast : false;

        public override int GetHashCode() =>
            ~Date.GetHashCode() * TemperatureC.GetHashCode();

        public static bool operator ==(WeatherForecast w1, WeatherForecast w2)
        {
            if (ReferenceEquals(w1, null) || ReferenceEquals(w2, null))
                return false;

            return w1.Date == w2.Date && w1.TemperatureC == w2.TemperatureC;
        }

        public static bool operator !=(WeatherForecast w1, WeatherForecast w2) =>
            !(w1 == w2);
    }
}
