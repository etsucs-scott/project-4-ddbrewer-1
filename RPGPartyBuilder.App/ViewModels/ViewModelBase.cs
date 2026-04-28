using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGPartyBuilder.App.ViewModels;

//  Base class for ViewModels that provides property change notifications for UI updates.
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    // Notifies the UI that a property value has changed.
    // CallerMemberName automatically uses the name of the calling property.
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}