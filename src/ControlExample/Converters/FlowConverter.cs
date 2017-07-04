using System;
using System.Windows.Data;

namespace TimeSpaceDiagram.Converters
{
    /// <summary>
    /// How wide do we make the traffic flow between inside the container?
    /// </summary>
    public class FlowConverter : IValueConverter
    {
        public double LeftBarWidth { get; set; }

        public double RightBarWidth { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dParentWidth = Double.Parse(value.ToString());
            double dAdjustedWidth = dParentWidth - LeftBarWidth - RightBarWidth - 10;
            return (dAdjustedWidth < 0 ? 0 : dAdjustedWidth);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
