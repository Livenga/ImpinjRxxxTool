using System;


namespace ImpinjRxxxTool.UhfRfid {
  /// <summary></summary>
  public class LLRPTimeoutException : Exception {
    /// <summary></summary>
    public LLRPTimeoutException(string message) : base(message) {
    }

    /// <summary></summary>
    public LLRPTimeoutException() : this(string.Empty) {
    }
  }
}
