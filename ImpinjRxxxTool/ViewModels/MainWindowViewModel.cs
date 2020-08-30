using System;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public class MainWindowViewModel : AbsViewModel {
    /// <summary></summary>
    public string Host {
      set => this.SetProperty(ref this.host, value, "Host");
      get => this.host;
    }

    /// <summary></summary>
    public int Port {
      set => this.SetProperty(ref this.port, value, "Port");
      get => this.port;
    }

    /// <summary></summary>
    public string ModelName {
      set => this.SetProperty(ref this.modelName, value, "ModelName");
      get => this.modelName;
    }

    /// <summary></summary>
    public string SerialNumber {
      set => this.SetProperty(ref this.serialNumber, value, "SerialNumber");
      get => this.serialNumber;
    }

    /// <summary></summary>
    public string SoftwareVersion {
      set => this.SetProperty(ref this.softwareVersion, value, "SoftwareVersion");
      get => this.softwareVersion;
    }

    /// <summary></summary>
    public string FirmwareVersion {
      set => this.SetProperty(ref this.firmwareVersion, value, "FirmwareVersion");
      get => this.firmwareVersion;
    }

    /// <summary></summary>
    public string FPGAVersion {
      set => this.SetProperty(ref this.fpgaVersion, value, "FPGAVersion");
      get => this.fpgaVersion;
    }

    /// <summary></summary>
    public string PCBAVersion {
      set => this.SetProperty(ref this.pcbaVersion, value, "PCBAVersion");
      get => this.pcbaVersion;
    }


    private string host = string.Empty;
    private int port = 5084;
    private string modelName = string.Empty;
    private string serialNumber = string.Empty;
    private string softwareVersion = string.Empty;
    private string firmwareVersion = string.Empty;
    private string fpgaVersion = string.Empty;
    private string pcbaVersion = string.Empty;
  }
}
