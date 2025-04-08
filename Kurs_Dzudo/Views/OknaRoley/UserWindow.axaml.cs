using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Kurs_Dzudo.ViewModels;
using Kurs_Dzudo.Views.OknaFunctiy;

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

    private void Izmenenia_Click(object sender, RoutedEventArgs e)
    {
        var login = new AddEditWindow();
        login.Show();
        this.Close();
    }

}