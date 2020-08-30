using System;

namespace ImpinjRxxxTool.Helpers {
  /// <summary></summary>
  public class RShellException : Exception {
    /// <summary></summary>
    public object? Tag {
      set => this.tag = value;
      get => this.tag;
    }

    private object? tag = null;

    /// <summary></summary>
    public RShellException(string message) : base(message) {
    }

    /// <summary></summary>
    public RShellException() : this(string.Empty) {
    }
  }
}
