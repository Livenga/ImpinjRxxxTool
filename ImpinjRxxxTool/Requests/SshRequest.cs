using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;


namespace ImpinjRxxxTool.Requests {
  /// <summary></summary>
  public class SshRequest {
    private static SshRequest? instance = null;

    /// <summary></summary>
    public static SshRequest GetInstance() {
      if(instance == null) {
        instance = new SshRequest();
      }

      return instance;
    }


    /// <summary></summary>
    public string Hostname {
      set => this.hostname = value;
      get => this.hostname;
    }


    /// <summary></summary>
    public int Port {
      set => this.port = value;
      get => this.port;
    }


    /// <summary></summary>
    public string Username {
      set => this.username = value;
      get => this.username;
    }


    /// <summary></summary>
    public string Password {
      set => this.password = value;
      private get => this.password;
    }


    private string hostname = string.Empty;
    private int port = 22;
    private string username = string.Empty;
    private string password = string.Empty;


    /// <summary></summary>
    public SshClient CreateClient() {
      PasswordAuthenticationMethod auth = new PasswordAuthenticationMethod(
          username: this.username,
          password: this.password);

      ConnectionInfo cInfo = new ConnectionInfo(
          this.hostname,
          this.port,
          this.username,
          new AuthenticationMethod[] { auth });

      return new SshClient(cInfo);
    }


    /// <summary></summary>
    public T Execute<T>(Func<SshClient, T> func) {
      using(SshClient client = this.CreateClient()) {
        client.Connect();
        T result = func(client);
        client.Disconnect();

        return result;
      }
    }


    /// <summary></summary>
    public void JustRun(Action<SshClient> action) {
      using(SshClient client = this.CreateClient()) {
        client.Connect();
        action(client);
        client.Disconnect();
      }
    }
  }
}
