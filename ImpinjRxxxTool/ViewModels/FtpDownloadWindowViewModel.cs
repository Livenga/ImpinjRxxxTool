using System;
using System.Collections.ObjectModel;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class FtpDownloadWindowViewModel : AbsViewModel {
    /// <summary></summary>
    public ObservableCollection<FtpDownloadItemControlViewModel> DownloadItems {
      set => this.SetProperty(ref this.downloadItems, value, "DownloadItems");
      get => this.downloadItems;
    }

    /// <summary></summary>
    public string TargetPath {
      set => this.SetProperty(ref this.targetPath, value, "TargetPath");
      get => this.targetPath;
    }


    private ObservableCollection<FtpDownloadItemControlViewModel> downloadItems =
      new ObservableCollection<FtpDownloadItemControlViewModel>();

    private string targetPath = string.Empty;
  }
}
