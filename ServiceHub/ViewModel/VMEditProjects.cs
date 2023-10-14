using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.ViewModel
{
    public class VMEditProjects: ObservableObject
    {
        private RestService rest;
        //imagem projeto convertida
        private ImageSource _image;
        public ImageSource ProjectImage 
        {
            get => _image;
            set => SetProperty(ref _image, value); 
        }

        //projeto
        public static Project PROJECT {  get; set; }     
        private Project _project;
        public Project Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        //imagem projeto bruto
        private byte[] _setimage;
        public byte[] SetImage
        {
            get => _setimage;
            set => SetProperty(ref _setimage, value);
        }

        //lista de integrantes
        private List<string> _integrantes;
        public List<string> Integrantes 
        {
            get { return _integrantes; }
            set { SetProperty(ref _integrantes, value); }
        }

        private string nomeintegrante;
        public string NomeIntegrante 
        {
            get => nomeintegrante;
            set => SetProperty(ref  nomeintegrante, value);
        }

        private string _users;
        public string Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        //buttons
        public IAsyncRelayCommand UpdateImage { get; set; }
        public IAsyncRelayCommand Update_Project { get; set; }
        public IAsyncRelayCommand AddIntegrantes { get; set; }
        public IAsyncRelayCommand DeleteIntegrantes { get; set; }

        public VMEditProjects()
        {
            rest = new RestService();
            this.Project = PROJECT;
            ProjectImage = ImageSource.FromStream(() => new MemoryStream(Project.Tasks.Image));
            UpdateImage = new AsyncRelayCommand(UpdateIMG);
            Update_Project = new AsyncRelayCommand(UpdateProject);
            AddIntegrantes = new AsyncRelayCommand(addIntegrante);
            DeleteIntegrantes = new AsyncRelayCommand(deleteintegrante);
            Integrantes = rest.AllUsers();
            Users = Project.Tasks.Users;
        }

        //funcoes para buttons
        private async Task UpdateIMG()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo == null) { return; }
                using Stream sourceStream = await photo.OpenReadAsync();
                using MemoryStream memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                byte[] byteArray = memoryStream.ToArray();
                SetImage = byteArray;
                Project.Tasks.Image = SetImage;
                ProjectImage = ImageSource.FromStream(() => new MemoryStream(Project.Tasks.Image));
            }
        }

        private async Task UpdateProject()
        {
            if (!string.IsNullOrEmpty(Project.Tasks.NameProject))
            {
                TaskModel setTask = Project.Tasks;
                setTask.Users = Users;
                if (await rest.UpdateProject(setTask))
                {
                    Toast toast = new Toast()
                    {
                        Text = "Projeto atualizado!"
                    };
                    await toast.Show();
                }
                else
                {
                    Toast toast = new Toast()
                    {
                        Text = "Falha ao atualizar projeto!"
                    };
                    await toast.Show();
                }
            }           
        }
        private async Task addIntegrante()
        {
            bool esta = false;
            foreach (var user in Integrantes)
            {
                if (user == nomeintegrante)
                {
                    esta = true;
                }
            }
            if (esta)
            {
                if (!string.IsNullOrEmpty(nomeintegrante))
                {
                    if (Users == "")
                    {
                        Users = nomeintegrante;
                    }
                    else
                    {
                        Users += $", {nomeintegrante}";
                    }
                }                
            }
            else
            {
                Toast toast = new()
                {
                    Text = "Nenhum Integrante encontrado"
                };
                await toast.Show();
            }
        }
        private async Task deleteintegrante()
        {           
            if (!string.IsNullOrEmpty(nomeintegrante))
            {
                if (Users.Contains(nomeintegrante))
                {
                    string[] usersArray = Users.Split(", ");
                    List<string> usersList = usersArray.ToList();

                    usersList.Remove(nomeintegrante);

                    if (usersList.Count > 1)
                    {
                        Users = string.Join(", ", usersList);
                    }
                    else if (usersList.Count == 1)
                    {
                        Users = usersList[0]; // Apenas um integrante, não adicione vírgula
                    }
                    else
                    {
                        Users = string.Empty; // Lista vazia
                    }
                }
            }
        }
    }
}
