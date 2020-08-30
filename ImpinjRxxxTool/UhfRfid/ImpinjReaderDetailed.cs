using System;


namespace ImpinjRxxxTool.UhfRfid {
  /// <summary></summary>
  public class ImpinjReaderDetailedVersion {
    /// <summary></summary>
    public string ModelName => this.modelName;

    /// <summary></summary>
    public string SerialNumber => this.serialNumber;

    /// <summary></summary>
    public string SoftwareVersion => this.softwareVersion;

    /// <summary></summary>
    public string FirmwareVersion => this.firmwareVersion;

    /// <summary></summary>
    public string FPGAVersion => this.fpgaVersion;

    /// <summary></summary>
    public string PCBAVersion => this.pcbaVersion;


    private readonly string modelName;
    private readonly string serialNumber;
    private readonly string softwareVersion;
    private readonly string firmwareVersion;
    private readonly string fpgaVersion;
    private readonly string pcbaVersion;


    /// <summary></summary>
    public ImpinjReaderDetailedVersion(
        string modelName,
        string serialNumber,
        string softwareVersion,
        string firmwareVersion,
        string fpgaVersion,
        string pcbaVersion) {
      this.modelName = modelName;
      this.serialNumber = serialNumber;
      this.softwareVersion = softwareVersion;
      this.firmwareVersion = firmwareVersion;
      this.fpgaVersion = fpgaVersion;
      this.pcbaVersion = pcbaVersion;
    }
  }
}
