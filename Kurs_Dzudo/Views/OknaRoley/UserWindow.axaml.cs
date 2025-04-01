using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kurs_Dzudo.ViewModels;

namespace Kurs_Dzudo;

public partial class UserWindow : Window
{
    public UserWindow()
    {
        InitializeComponent();
        DataContext = new ParticipantsViewModel(this);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

}