using ServiceHub.Model;
using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages;

public partial class ContactPage : ContentPage
{
	public ContactPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Button button = (Button) sender;
        VMcontacts vm = (VMcontacts)button.BindingContext;
        var user = vm.User;
        var numero = user.Telefone;
        await Launcher.Default.OpenAsync($"https://wa.me/55{numero}"); 
    }
}