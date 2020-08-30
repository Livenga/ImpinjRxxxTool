using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ImpinjRxxxTool.DataConverter {
  /// <summary></summary>
  public class DateTimeToStringConverter : IValueConverter {
    /// <summary></summary>
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) {
      string format = "yyyy-MM-ddTHH:mm:ss";
      if(parameter != null && parameter is string) {
        format = (string)parameter;
      }

      return value != null
        ? ((DateTime)value).ToString(format)
        : string.Empty;
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
