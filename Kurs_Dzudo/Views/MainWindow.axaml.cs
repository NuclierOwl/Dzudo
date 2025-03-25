using Avalonia.Controls;
using Avalonia.Interactivity;
using Kurs_Dzudo.Hardik.Connector;
using Kurs_Dzudo.Hardik.Dop;
using System.Linq;
using System.Threading.Tasks;

namespace Kurs_Dzudo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void ProverkaPorola_Click(object sender, RoutedEventArgs e)
    {
        string username = UserNameText.Text;
        string password = PassTextBox.Text;

        using (var context = new Connector())
        {
            var user = context.organizatori.FirstOrDefault(u => u.login == username && u.pass == password);

            if (user != null)
            {
                Window next = null;
                switch (user.pozition)
                {
                    case "Admin":
                        next = new AdminWindow();
                        break;
                    case "User":
                        next = new UserWindow();
                        break;
                    case "Gost":
                        next = new GestWindow();
                        break;
                    default:
                        await DopFunctii.ShowError(this, "Роль не найдена");
                        return;
                }

                if (next != null)
                {
                    next.Show();
                    this.Close();
                }
            }
            else
            {
                await DopFunctii.ShowError(this, "Пользователь не найден");
            }
        }
    }
}