using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMTaskPage : ObservableObject
    {
        private bool refresh;
        public bool Refresh
        {
            get => refresh;
            set => SetProperty(ref refresh, value);
        }
        private RestService rest;
        private List<Project> _projects;
        public List<Project> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }
        public IAsyncRelayCommand Refreshing { get; set; }
        public VMTaskPage()
        {
            rest = new RestService();
            getProject();
            Refreshing = new AsyncRelayCommand(refreshing);
        }
        private async Task getProject()
        {
            Projects = await rest.GetProjects(InicializeApp.User.Name);
        }
        private async Task refreshing()
        {
            Projects = await rest.GetProjects(InicializeApp.User.Name);
            Refresh = false;
        }
    }
    public class Project
    {
        public TaskModel Tasks { get; set; }
        public List<StageModel> Stages { get; set; }
        public ImageSource ImageSource
        {
            get
            {
                if (Tasks.Image != null && Tasks.Image.Length > 0)
                {
                    return ImageSource.FromStream(() => new MemoryStream(Tasks.Image));
                }
                return null;
            }
        }
    }
}
