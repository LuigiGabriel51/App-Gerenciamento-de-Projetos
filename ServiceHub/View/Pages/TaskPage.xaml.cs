using ServiceHub.Model;
using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages;

public partial class TaskPage : ContentPage
{
    public TaskPage()
    {
        InitializeComponent();
        if(InicializeApp.User.LevelPermission == Permission.NV1)
        {
            buttonEdit.IsVisible = false;
            buttonEdit.IsEnabled = false;
        }
    }

    private void ListViewShowCard(object sender, SelectedItemChangedEventArgs e)
    {
        card.IsVisible = true;
        Project project = (Project)e.SelectedItem;
        card.BindingContext = project;
        
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        card.BindingContext = null;
        listProjects.SelectedItem = null;
        card.IsVisible = false;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        Project bindingContext = (Project)button.BindingContext;
        VMEditProjects.PROJECT = bindingContext;
        var editPage = new EditProjectPage();
        Navigation.PushAsync(editPage);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if(card.IsVisible == true){
            card.BindingContext = null;
            listProjects.SelectedItem = null;
            card.IsVisible = false;
        }
    }

    private void buttonEditStages_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StageModel bindingContext = (StageModel)button.BindingContext;
        VMEditStage.ProjectStage = bindingContext;
        var editPage = new EditStagePage();
        Navigation.PushAsync(editPage);
    }
}