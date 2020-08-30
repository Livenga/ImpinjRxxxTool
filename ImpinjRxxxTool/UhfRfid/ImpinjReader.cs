using System;
using System.Diagnostics;
using System.Linq;

using Org.LLRP.LTK.LLRPV1;
using Org.LLRP.LTK.LLRPV1.DataType;
using Org.LLRP.LTK.LLRPV1.Impinj;


namespace ImpinjRxxxTool.UhfRfid {
  /// <summary></summary>
  public class ImpinjReader : IDisposable {
    /// <summary></summary>
    public bool IsConnected => this.llrpClient?.IsConnected ?? false;

    /// <summary></summary>
    public ImpinjReaderDetailedVersion? DetailedVersion => this.detailedVersion;

    /// <summary></summary>
    public string Host => this.host;

    /// <summary></summary>
    public int Port => this.port;

    /// <summary></summary>
    public int Timeout => this.timeout;

    /// <summary></summary>
    public string? MACAddress => this.macAddress;


    private readonly string host;
    private readonly int port;
    private readonly int timeout;

    private LLRPClient? llrpClient = null;
    private string? macAddress = null;
    private ImpinjReaderDetailedVersion? detailedVersion = null;


    /// <summary></summary>
    public ImpinjReader(
        string host,
        int port,
        int timeout) {
      this.host = host;
      this.port = port;
      this.timeout = timeout;
    }


    /// <summary></summary>
    public void Open() {
      if(this.IsConnected) {
        return;
      }

      this.llrpClient = new LLRPClient(port: this.port);

      ENUM_ConnectionAttemptStatusType status = ENUM_ConnectionAttemptStatusType.Success;
      bool isSuccessed = this.llrpClient.Open(
          llrp_reader_name: this.host,
          status: out status,
          timeout: this.timeout);

      if(! isSuccessed
          || status != ENUM_ConnectionAttemptStatusType.Success) {
        throw new Exception($"接続失敗. {status.ToString()}");
      }

      this.ResetToFactoryDefault();
      this.EnableImpinjExtetions();

      this.GetReaderConfig();
      this.GetReaderCapabilities();
    }


    /// <summary></summary>
    public void Close() {
      if(! this.IsConnected) {
        return;
      }

      this.llrpClient?.Close();
    }


    /// <summary></summary>
    public void Dispose() {
      this.Close();
    }


    /// <summary></summary>
    private void ResetToFactoryDefault() {
      MSG_SET_READER_CONFIG msg = new MSG_SET_READER_CONFIG();
      msg.ResetToFactoryDefault = true;

      MSG_ERROR_MESSAGE? msgErr = null;
      MSG_SET_READER_CONFIG_RESPONSE? msgResp = this.llrpClient?.SET_READER_CONFIG(
          msg: msg,
          msg_err: out msgErr,
          time_out: this.timeout);

      LLRPHelper.CheckError(msgResp, msgErr);
    }


    /// <summary></summary>
    private void EnableImpinjExtetions() {
      MSG_IMPINJ_ENABLE_EXTENSIONS msg = new MSG_IMPINJ_ENABLE_EXTENSIONS();
      MSG_ERROR_MESSAGE? msgErr = null;
      MSG_CUSTOM_MESSAGE? msgResp = this.llrpClient?.CUSTOM_MESSAGE(
          msg: msg,
          msg_err: out msgErr,
          time_out: this.timeout);

      LLRPHelper.CheckError(msgResp, msgErr);
    }


    /// <summary></summary>
    private void GetReaderConfig() {
      MSG_GET_READER_CONFIG msg = new MSG_GET_READER_CONFIG();
      msg.RequestedData = ENUM_GetReaderConfigRequestedData.All;

      PARAM_ImpinjRequestedData pRequestData = new PARAM_ImpinjRequestedData();
      msg.Custom.Add(pRequestData);
      pRequestData.RequestedData = ENUM_ImpinjRequestedDataType.All_Configuration;

      MSG_ERROR_MESSAGE? msgErr = null;
      MSG_GET_READER_CONFIG_RESPONSE? msgResp = this.llrpClient?.GET_READER_CONFIG(
          msg: msg,
          msg_err: out msgErr,
          time_out: this.timeout);

      LLRPHelper.CheckError(msgResp, msgErr);

      if(msgResp != null) {
        if(msgResp.Identification != null) {

          switch(msgResp.Identification.IDType) {
            case ENUM_IdentificationType.MAC_Address:
              this.macAddress = msgResp.Identification.ReaderID.ToHexString();
              break;
          }
        }
      }
    }

    /// <summary></summary>
    private void GetReaderCapabilities() {
      MSG_GET_READER_CAPABILITIES msg = new MSG_GET_READER_CAPABILITIES();
      msg.RequestedData = ENUM_GetReaderCapabilitiesRequestedData.All;

      PARAM_ImpinjRequestedData pRequestData = new PARAM_ImpinjRequestedData();
      msg.Custom.Add(pRequestData);
      pRequestData.RequestedData = ENUM_ImpinjRequestedDataType.All_Capabilities;


      MSG_ERROR_MESSAGE? msgErr = null;
      MSG_GET_READER_CAPABILITIES_RESPONSE? msgResp =
        this.llrpClient?.GET_READER_CAPABILITIES(
            msg: msg,
            msg_err: out msgErr,
            time_out: this.timeout);

      LLRPHelper.CheckError(msgResp, msgErr);

      if(msgResp != null) {
        for(int i = 0; i < msgResp.Custom.Length; ++i) {
          switch(msgResp.Custom[i]) {
            case PARAM_ImpinjDetailedVersion p:
              this.detailedVersion = new ImpinjReaderDetailedVersion(
                  modelName: p.ModelName,
                  serialNumber: p.SerialNumber,
                  softwareVersion: p.SoftwareVersion,
                  firmwareVersion: p.FirmwareVersion,
                  fpgaVersion: p.FPGAVersion,
                  pcbaVersion: p.PCBAVersion);
              break;
          }
        }
      }
    }
  }
}
