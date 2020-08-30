using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
using Microsoft.Win32;

using ImpinjRxxxTool.Requests;
using ImpinjRxxxTool.ViewModels;
using ImpinjRxxxTool.Windows;


namespace ImpinjRxxxTool.Controls {
  /// <summary>
  /// FtpControl.xaml
  /// </summary>
  public partial class FtpControl : UserControl {
    /// <summary></summary>
    public static readonly DependencyProperty HostnameProperty =
      DependencyProperty.Register(
          name: "Hostname",
          propertyType: typeof(string),
          ownerType: typeof(FtpControl));


    /// <summary></summary>
    public string Hostname {
      set => SetValue(HostnameProperty, value);
      get => (string)GetValue(HostnameProperty);
    }


    /// <summary></summary>
    public FtpControlViewModel ViewModel => (FtpControlViewModel)this.DataContext;


    /// <summary></summary>
    public FtpControl() {
      this.InitializeComponent();
      this.DataContext = new FtpControlViewModel();
    }


    /// <summary></summary>
    private void OnFtpControlLoaded(object source, RoutedEventArgs e) {
      this.FtpPassword.Password = "impinj";
    }


    /// <summary></summary>
    private void OnDownloadToZipClick(object source, RoutedEventArgs e) {
      string host = this.Hostname;
      string username = this.ViewModel.Username;
      string password = this.FtpPassword.Password;

      FtpDownloadWindow downloader = new FtpDownloadWindow();
      downloader.Owner = Window.GetWindow(this);

      downloader.Hostname = host;
      downloader.Port = 21;
      downloader.Username = username;
      downloader.Password = password;
      downloader.TargetPath = this.ViewModel.Path;


      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Filter = "Zip file(*.zip)|*zip|All files(*.*)|*.*";
      dialog.FilterIndex = 0;
      dialog.RestoreDirectory = true;
      dialog.FileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.zip";

      bool result = dialog.ShowDialog() ?? false;
      if(result) {
        using(Stream stream = dialog.OpenFile()) {
          downloader.OutputStream = stream;
          downloader.ShowDialog();
        }
      }

      System.GC.Collect();
    }
  }
}
