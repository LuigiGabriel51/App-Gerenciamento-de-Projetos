using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages;

public partial class EditStagePage : ContentPage
{
    public EditStagePage()
    {
        InitializeComponent();
    }


    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (key.IsToggled)
        {
            VMEditStage.conclusao = true;
            conclusao.Text = "Concluído";
            conclusao.TextColor = Colors.Green;
        }
        else
        {
            VMEditStage.conclusao = false;
            conclusao.Text = "Em andamento";
            conclusao.TextColor = Colors.Red;
        }
    }
}