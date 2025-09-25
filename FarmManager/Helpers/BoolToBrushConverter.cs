using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FarmManager.App.Helpers;
public class BoolToBrushConverter : IValueConverter
{
    public Brush CurrentMonthBrush { get; set; } = Brushes.White;
    public Brush OtherMonthBrush { get; set; } = Brushes.LightGray;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value is bool b && b) ? CurrentMonthBrush : OtherMonthBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
