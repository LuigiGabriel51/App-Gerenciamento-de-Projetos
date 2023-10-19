using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages;

public partial class EditStagePage : ContentPage
{
    public EditStagePage()
    {
        InitializeComponent();
        date.MinimumDate = DateTime.Now;
    }

    private void key_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBox check = sender as CheckBox;
        if (check.IsChecked)
        {
            VMEditStage.conclusao = true;
        }
        else
        {
            VMEditStage.conclusao = false;
        }
    }
}