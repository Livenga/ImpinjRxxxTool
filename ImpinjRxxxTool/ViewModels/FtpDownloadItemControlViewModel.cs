using System;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class FtpDownloadItemControlViewModel : AbsViewModel {
    /// <summary></summary>
    public bool IsCompleted {
      set => this.SetProperty(ref this.isCompleted, value, "IsCompleted");
      get => this.isCompleted;
    }

    /// <summary></summary>
    public string Item {
      set => this.SetProperty(ref this.item, value, "Item");
      get => this.item;
    }

    /// <summary></summary>
    public DateTime? DownloadedAt {
      set => this.SetProperty(ref this.downloadedAt, value, "DownloadedAt");
      get => this.downloadedAt;
    }


    private bool isCompleted = false;
    private string item = string.Empty;
    private DateTime? downloadedAt = null;
  }
}
