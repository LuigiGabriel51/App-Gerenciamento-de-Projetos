using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ServiceHub.Model;
using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void push_mensagens(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HelpPage");
    }

    private async void projetos(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var model = button.BindingContext as VMHomePage; // Substitua 'SeuModeloDeDados' pelo tipo correto do seu modelo
        if (model != null)
        {
            string cargo = model.Cargo;

            if (cargo == "Chefe" || cargo == "Gerente")
            {
                await Navigation.PushAsync(new AddProjectPage());
            }
            else if (cargo == "Funcionário")
            {
                await Shell.Current.GoToAsync("///TaskPage");
            }
        }
    }

    private async void EventEmployes(object sender, EventArgs e)
    {
        if(VMfuncionarios.UserModels == null) 
        {
            Toast t = new Toast() { Text = "Carregando funcionários, aguarde..." , Duration = ToastDuration.Long};
            await t.Show();
            return;
        }
        var page = new FuncionariosPage(InicializeApp.User.LevelPermission);
        await Navigation.PushAsync(page);
    }
}