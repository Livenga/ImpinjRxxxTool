using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ImpinjRxxxTool.DataConverter {
  /// <summary></summary>
  public class BooleanToVisibilityConverter : IValueConverter {
    /// <summary></summary>
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) {
      Visibility v = Visibility.Collapsed;
      bool b = (value is bool) ? (bool)value : false;

      if(parameter != null && parameter is string) {
        v = (Visibility)Enum.Parse(typeof(Visibility), (string)parameter);
      }

      return b ? Visibility.Visible : v;
    }


    /// <summary></summary>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) {
      throw new NotImplementedException();
    }

  }
}
