using System;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class FtpControlViewModel : AbsViewModel {
    /// <summary></summary>
    public int Port {
      set => this.SetProperty(ref this.port, value, "Port");
      get => this.port;
    }

    /// <summary></summary>
    public string Username {
      set => this.SetProperty(ref this.username, value, "Username");
      get => this.username;
    }

    /// <summary></summary>
    public string Path {
      set => this.SetProperty(ref this.path, value, "Path");
      get => this.path;
    }

    /// <summary></summary>
    public bool IsBinaryMode {
      set => this.SetProperty(ref this.isBinaryMode, value, "IsBinaryMode");
      get => this.isBinaryMode;
    }


    private int port = 21;
    private string username = "root";
    private string path = "/mnt/spp/log";
    private bool isBinaryMode = false;
  }
}
