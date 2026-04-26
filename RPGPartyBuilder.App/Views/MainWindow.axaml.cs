using Avalonia.Controls;
using Avalonia.Interactivity;
using RPGPartyBuilder.App.ViewModels;

namespace RPGPartyBuilder.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void AddCharacterButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.AddCharacter();
        }
    }

    private void RemoveCharacterButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.RemoveCharacter();
        }
    }

    private void LevelUpButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.LevelUpSelectedCharacter();
        }
    }

    private void SavePartyButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.SaveParty();
        }
    }

    private void LoadPartyButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.LoadParty();
        }
    }
    
    private void SortByLevelButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.SortPartyByLevel();
        }
    }
}