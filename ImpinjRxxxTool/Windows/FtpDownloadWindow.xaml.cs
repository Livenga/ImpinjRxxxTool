using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.IO.Compression;
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

using ImpinjRxxxTool.Requests;
using ImpinjRxxxTool.ViewModels;


namespace ImpinjRxxxTool.Windows {
  /// <summary>
  /// FtpDownloadWindow.xaml の相互作用ロジック
  /// </summary>
  public partial class FtpDownloadWindow : Window {
    public string Hostname {
      set => this.hostname = value;
      get => this.hostname;
    }

    /// <summary></summary>
    public int Port {
      set => this.port = value;
      get => this.port;
    }

    /// <summary></summary>
    public string Username {
      set => this.username = value;
      get => this.username;
    }

    /// <summary></summary>
    public string Password {
      set => this.password = value;
      get => this.password;
    }

    /// <summary></summary>
    public string TargetPath {
      set => this.targetPath = value;
      get => this.targetPath;
    }

    /// <summary></summary>
    public Stream? OutputStream {
      set => this.outputStream = value;
      get => this.outputStream;
    }


    /// <summary></summary>
    public FtpDownloadWindowViewModel ViewModel =>
      (FtpDownloadWindowViewModel)this.DataContext;


    private string hostname = string.Empty;
    private int port = 21;
    private string username = string.Empty;
    private string password = string.Empty;
    private string targetPath = string.Empty;

    private Stream? outputStream = null;


    /// <summary></summary>
    public FtpDownloadWindow() {
      this.InitializeComponent();
      this.DataContext = new FtpDownloadWindowViewModel();
    }


    /// <summary></summary>
    private async void StartDownload() {
      if(this.outputStream == null) {
        throw new NullReferenceException();
      }

      FtpRequest request = FtpRequest.GetInstance();
      string? downloadingItem = null;

      try {
        string[] items = await Task.Run(() => request.ListDirectory(path: this.targetPath));

        this.ViewModel.DownloadItems.Clear();
        foreach(string item in items) {
          this.ViewModel.DownloadItems.Add(new FtpDownloadItemControlViewModel() { Item = item });
        }

        using(ZipArchive archive = new ZipArchive(this.outputStream, ZipArchiveMode.Create)) {
          foreach(string item in items) {
            downloadingItem = item;
            this.ViewModel.TargetPath = item;

            string entryName = item.Split(new char[] { '/' }).Last();
            ZipArchiveEntry entry = archive.CreateEntry(entryName);

            using(Stream es = entry.Open()) {
              await Task.Run(() => request.Download(item, es));

              var vm = this.ViewModel.DownloadItems.First(i => i.Item == item);
              vm.IsCompleted = true;
              vm.DownloadedAt = DateTime.Now;
            }
          }
        }
      } catch(Exception except) {
        MessageBox.Show(
            caption: "Download Error",
            messageBoxText: $"{downloadingItem ?? string.Empty}\n{except.Message}",
            icon: MessageBoxImage.Error,
            button: MessageBoxButton.OK);

        DBG.WriteLine(except);
      } finally {
        this.DialogResult = true;
      }
    }


    /// <summary></summary>
    private void OnWindowLoaded(object source, RoutedEventArgs e) {
      if(this.outputStream == null) {
        this.DialogResult = false;
        return;
      }

      FtpRequest req = FtpRequest.GetInstance();

      req.Hostname = this.hostname;
      req.Port = this.port;
      req.Username = this.username;
      req.Password = this.password;

      this.StartDownload();
    }
  }
}
