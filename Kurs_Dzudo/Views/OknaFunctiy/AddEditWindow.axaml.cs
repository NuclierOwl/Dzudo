using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Kurs_Dzudo.Hardik.Connector.Date;

namespace Kurs_Dzudo.Views.OknaFunctiy;

public partial class AddEditWindow : Window
{
    public UkhasnikiDao Participant { get; set; }

    public AddEditWindow()
    {
        InitializeComponent();
        Participant = new UkhasnikiDao();
        DataContext = Participant;
    }

    public AddEditWindow(UkhasnikiDao participant)
    {
        InitializeComponent();
        Participant = participant;
        DataContext = Participant;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close(false);
    }


}