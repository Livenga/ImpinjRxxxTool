using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;


namespace ImpinjRxxxTool.Requests {
  /// <summary></summary>
  public class FtpRequest {
    /// <summary></summary>
    public static FtpRequest? instance = null;

    /// <summary></summary>
    public string Hostname {
      set => this.hostname = value;
      get => this.hostname;
    }

    /// <summary></summary>
    public string Username {
      set => this.username = value;
      get => this.username;
    }

    /// <summary></summary>
    public string Password {
      set => this.password = value;
      get => this.password;
    }

    /// <summary></summary>
    public int Port {
      set => this.port = value;
      get => this.port;
    }


    private string hostname = string.Empty;
    private string username = string.Empty;
    private string password = string.Empty;
    private int port = 21;


    /// <summary></summary>
    public static FtpRequest GetInstance() {
      if(instance == null) {
        instance = new FtpRequest();
      }

      return instance;
    }


    /// <summary></summary>
    private FtpWebRequest Create(
        string method,
        string uri,
        bool isBinaryMode) {
      FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

      request.Method = method;
      request.UseBinary = isBinaryMode;
      request.Credentials = new NetworkCredential(
          userName: this.username,
          password: this.password);

      return request;
    }


    /// <summary></summary>
    private byte[] StreamToByteArray(Stream stream) {
      using(MemoryStream memory = new MemoryStream()) {
        int readSize = 0;
        byte[] buffer = new byte[4096];

        while((readSize = stream.Read(buffer, 0, 4096)) > 0) {
          memory.Write(buffer, 0, readSize);
        }

        return memory.ToArray();
      }
    }


    /// <summary></summary>
    public long GetFileSize(string path) {
      FtpWebRequest request = Create(
          method: WebRequestMethods.Ftp.GetFileSize,
          uri: $"ftp://{this.hostname}:{this.port}/{path}",
          isBinaryMode: true);

      using(FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
        return response.ContentLength;
      }
    }


    /// <summary>パスに含まれる要素一覧の取得</summary>
    public string[] ListDirectory(string path) {
      string uri = $"ftp://{this.hostname}:{this.port}/{path}";
#if DEBUG
      Debug.WriteLine($"[Debug] {uri}");
#endif

      string basePath = path;
      if(path[path.Length - 1] != '/') {
        string[] arr = path.Split(new char[] { '/' });
        string[] dest = new string[arr.Length - 1];
        Array.Copy(arr, 0, dest, 0, arr.Length - 1);

        basePath = string.Join("/", dest);
      } else {
        basePath = path.Remove(path.Length - 1, 1);
      }

      FtpWebRequest request = Create(
          method: WebRequestMethods.Ftp.ListDirectory,
          uri: uri,
          isBinaryMode: false);

      List<string> items = new List<string>();
      using(FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
        using(Stream stream = response.GetResponseStream()) {
          using(StreamReader reader = new StreamReader(
                stream: stream,
                encoding: Encoding.UTF8)) {
            string? line = null;

            while((line = reader.ReadLine()) != null) {
              items.Add($"{basePath}/{line}");
            }
          } 
        }
      }

      return items.ToArray();
    }


    /// <summary></summary>
    public byte[] Get(string path, bool isBinaryMode) {
      FtpWebRequest request = Create(
          method: WebRequestMethods.Ftp.DownloadFile,
          uri: $"ftp://{this.hostname}:{this.port}/{path}",
          isBinaryMode: isBinaryMode);

      using(FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
        using(Stream stream = response.GetResponseStream()) {
          return StreamToByteArray(stream);
        }
      }
    }


    /// <summary></summary>
    public void Download(string path, Stream dest) {
      FtpWebRequest request = Create(
          method: WebRequestMethods.Ftp.DownloadFile,
          uri: $"ftp://{this.hostname}:{this.port}/{path}",
          isBinaryMode: true);

      using(FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
        using(Stream stream = response.GetResponseStream()) {
          int readSize = 0;
          byte[] buffer = new byte[4096];

          while((readSize = stream.Read(buffer, 0, 4096)) > 0) {
            dest.Write(buffer, 0, readSize);
          }
        }
      }
    }
  }
}
