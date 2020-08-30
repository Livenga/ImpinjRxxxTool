using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ImpinjRxxxTool.ViewModels {
  /// <summary></summary>
  public abstract class AbsViewModel : INotifyPropertyChanged {
    /// <summary></summary>
    public event PropertyChangedEventHandler? PropertyChanged = null;


    /// <summary></summary>
    protected void SetProperty<T>(
        ref T storage,
        T value,
        [CallerMemberName]string? propertyName = null) {
      if(object.ReferenceEquals(storage, value)) {
        return;
      }

      storage = value;
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
