using System;
using System.Diagnostics;
using System.Reflection;

using Org.LLRP.LTK.LLRPV1;
using Org.LLRP.LTK.LLRPV1.DataType;
using Org.LLRP.LTK.LLRPV1.Impinj;


namespace ImpinjRxxxTool.UhfRfid {
  /// <summary></summary>
  public static class LLRPHelper {
    public static void CheckError(Message? msg, MSG_ERROR_MESSAGE? msgErr) {
      if(msg == null && msgErr == null) {
        throw new LLRPTimeoutException();
      }

      PARAM_LLRPStatus? status = (PARAM_LLRPStatus?)msg?.GetType()
        .GetField(name: "LLRPStatus")
        .GetValue(msg);

      if(msgErr != null) {
        status = msgErr.LLRPStatus;
      }

      if(status == null) {
        throw new InvalidOperationException();
      }

      if(status.StatusCode != ENUM_StatusCode.M_Success) {
        throw new LLRPException(status.StatusCode, status.ErrorDescription);
      }
    }
  }
}
