using System;
using System.Windows.Data;

namespace TimeSpaceDiagram.Converters
{
    /// <summary>
    /// TODO Extract interface so we can create a metric version
    /// TODO Add Units property
    /// TODO Create an abstract UnitsPerSecond method
    /// </summary>
    public class SpeedLimitAngleConverter : IMultiValueConverter
    {
        private const double FeetPerMilePerSecondsPerHour = (double) 5280 / 3600;

        public double Distance { get; set; }
        public double SpeedLimit { get; set; }
        public double CycleLength { get; set; }
        
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double unitsPerSecond = FeetPerMilePerSecondsPerHour * SpeedLimit;
            double seconds = Distance / unitsPerSecond;
            double secondsInPixels = (seconds / CycleLength) * (double)values[0];
            double angle = (180 / Math.PI) * Math.Atan2(secondsInPixels, (double)values[1]); ;
            return angle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
