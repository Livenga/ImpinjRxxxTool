using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;

using ImpinjRxxxTool.Requests;


namespace ImpinjRxxxTool.Helpers {
  /// <summary></summary>
  public static class RShellHelper {
    private static readonly Regex regex = new Regex(
        pattern: "^(?<key>[^']*)='(?<value>.*)'$",
        options: RegexOptions.Compiled);


    /// <summary></summary>
    public static Dictionary<string, string> Convert(string value) {
      using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(value))) {
        return Convert(stream);
      }
    }


    /// <summary></summary>
    public static Dictionary<string, string> Convert(Stream stream) {
      Dictionary<string, string> d = new Dictionary<string, string>();

      using(StreamReader reader = new StreamReader(stream)) {
        string? line = null;

        while((line = reader.ReadLine()) != null) {
          Match m = regex.Match(input: line);
          if(! m.Success) {
            throw new RShellException();
          }

          d.Add(
              key: m.Groups["key"].Value,
              value: m.Groups["value"].Value);
        }
      }

      return d;
    }
  }
}
