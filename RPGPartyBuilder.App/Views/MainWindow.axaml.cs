using Avalonia.Controls;
using Avalonia.Interactivity;
using RPGPartyBuilder.App.ViewModels;

namespace RPGPartyBuilder.App.Views;

// Code-behind for the main application window. Handles UI events and forwards them to the ViewModel.
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // Sets the ViewModel for data binding.
        DataContext = new MainWindowViewModel();
    }

    // Called when the "Add Character" button is clicked.
    private void AddCharacterButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.AddCharacter();
        }
    }

    // Called when the "Remove Character" button is clicked.
    private void RemoveCharacterButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.RemoveCharacter();
        }
    }

    // Called when the "Level Up" button is clicked.
    private void LevelUpButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.LevelUpSelectedCharacter();
        }
    }

    // Called when the "Save Party" button is clicked.
    private void SavePartyButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.SaveParty();
        }
    }

    // Called when the "Load Party" button is clicked.
    private void LoadPartyButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.LoadParty();
        }
    }
    
    // Called when the "Sort by Level" button is clicked.
    private void SortByLevelButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.SortPartyByLevel();
        }
    }
}