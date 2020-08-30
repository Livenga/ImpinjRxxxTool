using System;
using System.Linq;

using ImpinjRxxxTool.Models;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class SshControlViewModel : AbsViewModel {
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
    public ImpinjSshRequestModel SelectedRequest {
      set => this.SetProperty(ref this.selectedRequest, value, "SelectedRequest");
      get => this.selectedRequest;
    }

    /// <summary></summary>
    public string SshInlineCommand {
      set => this.SetProperty(ref this.sshInlineCommand, value, "SshInlineCommand");
      get => this.sshInlineCommand;
    }

    /// <summary></summary>
    public bool IsProcessingCommand {
      set => this.SetProperty(ref this.isProcessingCommand, value, "IsProcessingCommand");
      get => this.isProcessingCommand;
    }


    private int port = 22;
    private string username = "root";
    private ImpinjSshRequestModel selectedRequest =
      ImpinjRxxxTool.Models.ImpinjSshRequestModel.All.First();
    private string sshInlineCommand = string.Empty;
    private bool isProcessingCommand = false;
  }
}
