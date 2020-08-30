using System;

using ImpinjRxxxTool.DataType;


namespace ImpinjRxxxTool.Models {
  /// <summary></summary>
  public class ImpinjSshRequestModel {
    /// <summary></summary>
    public static ImpinjSshRequestModel[] All { private set; get; }

    static ImpinjSshRequestModel() {
      All = new ImpinjSshRequestModel[] {
        new ImpinjSshRequestModel(ImpinjSshRequest.ShowNetworkIpSummary),
        new ImpinjSshRequestModel(ImpinjSshRequest.ShowNetworkIpStat)
      };
    }


    /// <summary></summary>
    public ImpinjSshRequest Value => this.value;

    /// <summary></summary>
    public string Text => this.ToString();

    /// <summary></summary>
    public string CommandText {
      get {
        switch(this.value) {
          case ImpinjSshRequest.ShowNetworkIpSummary:
            return "show network ip summary";

          case ImpinjSshRequest.ShowNetworkIpStat:
            return "show network ip stat";
        }

        throw new NotImplementedException();
      }
    }


    private ImpinjSshRequest value;


    /// <summary></summary>
    public ImpinjSshRequestModel(ImpinjSshRequest value) {
      this.value = value;
    }


    /// <summary></summary>
    public override string ToString() {
      return ToString(this.value);
    }

    /// <summary></summary>
    public static string ToString(ImpinjSshRequest request) {
      switch(request) {
        case ImpinjSshRequest.ShowNetworkIpSummary:
          return "Show Network IP Summary";
        case ImpinjSshRequest.ShowNetworkIpStat:
          return "Show Network IP Stat";
      }

      return string.Empty;
    }
  }
}
