using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

using ImpinjRxxxTool.UhfRfid;
using ImpinjRxxxTool.ViewModels;


namespace ImpinjRxxxTool.Windows {
  /// <summary>
  /// MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    /// <summary></summary>
    public MainWindowViewModel ViewModel => (MainWindowViewModel)this.DataContext;


    /// <summary></summary>
    public MainWindow() {
      this.InitializeComponent();
      this.DataContext = new MainWindowViewModel();
    }

    /// <summary></summary>
    private ImpinjReaderDetailedVersion? GetImpinjReaderDetals(string host, int port) {
      using(ImpinjReader reader = new ImpinjReader(
            host: host,
            port: port,
            timeout: 3000)) {
        reader.Open();

#if DEBUG
        Debug.WriteLine($"[DEBUG] MAC Address: {reader.MACAddress}");
#endif
        if(reader.DetailedVersion != null) {
          return reader.DetailedVersion;
        }
      }

      return null;
    }


    /// <summary></summary>
    private void OnWindowUnloaded(object source, RoutedEventArgs e) {
      if(this.SshControl.SshResultWindow != null
          && this.SshControl.SshResultWindow.IsActive) {
        this.SshControl.SshResultWindow.Close();
      }
    }


    /// <summary></summary>
    private async void OnGetDetailsClick(object source, RoutedEventArgs e) {
      Button btn = (Button)source;

      try {
        btn.IsEnabled = false;

        string host = this.ViewModel.Host;
        int port = this.ViewModel.Port;

        var detailed = await Task.Run(() => this.GetImpinjReaderDetals(host, port));
        if(detailed != null) {
          this.ViewModel.ModelName = detailed.ModelName;
          this.ViewModel.SerialNumber = detailed.SerialNumber;
          this.ViewModel.SoftwareVersion = detailed.SoftwareVersion;
          this.ViewModel.FirmwareVersion = detailed.FirmwareVersion;
          this.ViewModel.FPGAVersion = detailed.FPGAVersion;
          this.ViewModel.PCBAVersion = detailed.PCBAVersion;
        }
      } catch(Exception except) {
#if DEBUG
        DBG.WriteLine(except);
#endif
      } finally {
        btn.IsEnabled = true;
      }
    }
  }
}
