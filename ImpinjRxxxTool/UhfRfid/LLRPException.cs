using System;

using Org.LLRP.LTK.LLRPV1;
using Org.LLRP.LTK.LLRPV1.DataType;


namespace ImpinjRxxxTool.UhfRfid {
  /// <summary></summary>
  public class LLRPException : Exception {
    /// <summary></summary>
    public ENUM_StatusCode StatusCode => this.statusCode;

    /// <summary></summary>
    public string ErrorDescription => this.errorDescription;

    private readonly ENUM_StatusCode statusCode;
    private readonly string errorDescription;


    /// <summary></summary>
    public LLRPException(
        ENUM_StatusCode statusCode,
        string errorDescription) : base(errorDescription) {
      this.statusCode = statusCode;
      this.errorDescription = errorDescription;
    }
  }
}
