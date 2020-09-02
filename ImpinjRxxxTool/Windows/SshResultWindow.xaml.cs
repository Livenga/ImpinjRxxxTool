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
using System.Windows.Shapes;

using ImpinjRxxxTool.Helpers;
using ImpinjRxxxTool.ViewModels;


namespace ImpinjRxxxTool.Windows {
  /// <summary>
  /// SshResultWindow.xaml
  /// </summary>
  public partial class SshResultWindow : Window {
    /// <summary></summary>
    public static readonly DependencyProperty ResultTextProperty =
      DependencyProperty.Register(
          name: "ResultText",
          propertyType: typeof(string),
          ownerType: typeof(SshResultWindow),
          typeMetadata: new PropertyMetadata(
            defaultValue: string.Empty,
            propertyChangedCallback: OnResultTextChanged));


    /// <summary></summary>
    public string ResultText {
      set => SetValue(ResultTextProperty, value);
      get => (string)GetValue(ResultTextProperty);
    }


    /// <summary></summary>
    public SshResultWindowViewModel ViewModel =>
      (SshResultWindowViewModel)this.DataContext;


    /// <summary></summary>
    public SshResultWindow() {
      this.InitializeComponent();
      this.DataContext = new SshResultWindowViewModel();
    }


    /// <summary></summary>
    private void OnWindowLoaded(object source, RoutedEventArgs e) {
    }


    /// <summary></summary>
    private static void OnResultTextChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e) {
      SshResultWindow self = (SshResultWindow)d;


      try {
        self.ViewModel.Result = RShellHelper.Convert(value: self.ResultText);
      } catch(Exception except) {
        // [Key]='[Value]' 形式で Ssh の結果が得られなかった場合,
        // DataGrid の表示内容をクリアする.
        // RShell にて ? を用いたヘルプの表示等で発生する.
        self.ViewModel.Result.Clear();
        self.ResultDataGrid.Items.Refresh();

#if DEBUG
        DBG.WriteLine(except);
#endif
      } finally {
        System.GC.Collect();
      }
    }
  }
}
