using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Renci.SshNet;

using ImpinjRxxxTool.Helpers;
using ImpinjRxxxTool.Requests;
using ImpinjRxxxTool.ViewModels;
using ImpinjRxxxTool.Windows;


namespace ImpinjRxxxTool.Controls {
  /// <summary>
  /// SshControl.xaml
  /// </summary>
  public partial class SshControl : UserControl {
    /// <summary></summary>
    public static readonly DependencyProperty HostnameProperty =
      DependencyProperty.Register(
          name: "Hostname",
          propertyType: typeof(string),
          ownerType: typeof(SshControl));


    /// <summary></summary>
    public string Hostname {
      set => SetValue(HostnameProperty, value);
      get => (string)GetValue(HostnameProperty);
    }


    /// <summary></summary>
    public SshControlViewModel ViewModel => (SshControlViewModel)this.DataContext;

    private SshResultWindow? resultWindow = null;


    /// <summary></summary>
    public SshControl() {
      this.InitializeComponent();
      this.DataContext = new SshControlViewModel();
    }

    /// <summary></summary>
    private SshRequest GetSshRequest() {
      SshRequest req = SshRequest.GetInstance();

      req.Hostname = this.Hostname;
      req.Port = this.ViewModel.Port;
      req.Username = this.ViewModel.Username;
      req.Password = this.SshPassword.Password;

      return req;
    }

    /// <summary></summary>
    private string SshExecute(SshClient client, string commandText) {
      string result;

      using(SshCommand cmd = client.CreateCommand(commandText)) {
        result = cmd.Execute();
      }

      return result;
    }


    /// <summary></summary>
    private void DisplaySshResultWindow(string commandText, string result) {
      if(this.resultWindow == null || ! this.resultWindow.IsLoaded) {
        this.resultWindow = new SshResultWindow();
        //this.resultWindow.Owner = Window.GetWindow(this);
      }

      this.resultWindow.ViewModel.CommandText = commandText;
      this.resultWindow.ResultText = result;

      this.resultWindow.Show();
    }


    /// <summary></summary>
    private void OnControlLoaded(object source, RoutedEventArgs e) {
      this.SshPassword.Password = "impinj";
    }


    /// <summary></summary>
    private async void OnRequestCommandExecuteClick(object source, RoutedEventArgs e) {
      SshRequest req = this.GetSshRequest();
      string commandText = this.ViewModel.SelectedRequest.CommandText;

      try {
        this.ViewModel.IsProcessingCommand = true;

        string result = await Task.Run(() => req.Execute((client) => SshExecute(client, commandText)));

        this.DisplaySshResultWindow(commandText, result);
      } catch(Exception except) {
        DBG.WriteLine(except);
      } finally {
        this.ViewModel.IsProcessingCommand = false;
      }
    }


    /// <summary></summary>
    private async void OnExecuteClick(object source, RoutedEventArgs e) {
      if(this.ViewModel.SshInlineCommand.Length == 0) {
        return;
      }

      SshRequest req = this.GetSshRequest();
      string cmdText = this.ViewModel.SshInlineCommand;

      try {
        this.ViewModel.IsProcessingCommand = true;

        string result = await Task.Run(() => req.Execute((cli) => SshExecute(cli, cmdText)));

        this.DisplaySshResultWindow(cmdText, result);
      } catch(Exception except) {
        DBG.WriteLine(except);
      } finally {
        this.ViewModel.IsProcessingCommand = false;
      }
    }
  }
}
