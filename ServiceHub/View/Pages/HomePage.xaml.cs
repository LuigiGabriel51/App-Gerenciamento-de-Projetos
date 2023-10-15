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
                await Navigation.PushAsync(new AddProjectsPage());
            }
            else if (cargo == "Funcionário")
            {
                await Shell.Current.GoToAsync("///TaskPage");
            }
        }
    }

    private void EventEmployes(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var model = button.BindingContext as VMHomePage; // Substitua 'SeuModeloDeDados' pelo tipo correto do seu modelo
        if (model != null)
        {
            string cargo = model.Cargo;

            if (cargo == "Chefe" || cargo == "Gerente")
            {
                //await Navigation.PushAsync(new UpdateEmployes());
            }
            else if (cargo == "Funcionário")
            {
                //await Navigation.PushAsync(new ViewEmployes());
            }
        }
    }
}