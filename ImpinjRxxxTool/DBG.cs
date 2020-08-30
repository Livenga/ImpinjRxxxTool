using System;
using System.Diagnostics;


namespace ImpinjRxxxTool {
  /// <summary></summary>
  public static class DBG {
    /// <summary></summary>
    public static void WriteLine(string message) {
      Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}] {message}");
    }


    /// <summary></summary>
    public static void WriteLine(Exception e) {
      DBG.WriteLine($"{e.GetType().Name} [{e.Message}] [{e.StackTrace}]");
    }
  }
}
