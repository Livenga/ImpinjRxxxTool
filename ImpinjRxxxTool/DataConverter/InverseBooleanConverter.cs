using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ImpinjRxxxTool.DataConverter {
  /// <summary></summary>
  public class InverseBooleanConverter : IValueConverter {
    /// <summary></summary>
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) {
      if(value == null || ! (value is bool)) {
        return false;
      }

      return ! (bool)value;
    }


    /// <summary></summary>
    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture) {
      if(value == null || ! (value is bool)) {
        return false;
      }

      return ! (bool)value;
    }

  }
}
