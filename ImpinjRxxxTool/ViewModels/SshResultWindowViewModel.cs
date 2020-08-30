using System;
using System.Collections.Generic;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class SshResultWindowViewModel : AbsViewModel {
    /// <summary></summary>
    public string CommandText {
      set => this.SetProperty(ref this.commandText, value, "CommandText");
      get => this.commandText;
    }

    /// <summary></summary>
    public Dictionary<string, string> Result {
      set => this.SetProperty(ref this.result, value, "Result");
      get => this.result;
    }


    private string commandText = string.Empty;
    private Dictionary<string, string> result = new Dictionary<string, string>();
  }
}
