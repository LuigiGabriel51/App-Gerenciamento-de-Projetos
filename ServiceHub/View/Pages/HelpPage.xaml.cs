namespace ServiceHub.View.Pages;

public partial class HelpPage : ContentPage
{
    RestService Rest;
    public HelpPage()
    {
        InitializeComponent();
        Rest = new RestService();
    }
}