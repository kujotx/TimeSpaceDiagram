using System;
using System.Windows.Data;

namespace TimeSpaceDiagram.Converters
{
    public class CycleConverter : IValueConverter
    {
        public int Cycles { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double cycle = (this.Cycles == 0 ? 1 : this.Cycles);
            return 1 / cycle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CycleBackgroundConverter : IValueConverter
    {
        public int Cycles { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double cycle = (this.Cycles == 0 ? 1 : this.Cycles);
            return 2 / cycle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
