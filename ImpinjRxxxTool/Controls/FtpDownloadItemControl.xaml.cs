using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImpinjRxxxTool.Controls {
  /// <summary>
  /// FtpDownloadItemControl.xaml
  /// </summary>
  public partial class FtpDownloadItemControl : UserControl {
    /// <summary></summary>
    public static readonly DependencyProperty ItemProperty =
      DependencyProperty.Register(
          name: "Item",
          propertyType: typeof(string),
          ownerType: typeof(FtpDownloadItemControl));


    /// <summary></summary>
    public string Item {
      set => SetValue(ItemProperty, value);
      get => (string)GetValue(ItemProperty);
    }


    /// <summary></summary>
    public FtpDownloadItemControl() {
      this.InitializeComponent();
    }
  }
}
