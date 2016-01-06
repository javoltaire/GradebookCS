using System;
using Windows.UI.Xaml.Data;

namespace GradebookCS.View.Converters
{
    public class TwoDecimalPlacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Math.Round((double)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
