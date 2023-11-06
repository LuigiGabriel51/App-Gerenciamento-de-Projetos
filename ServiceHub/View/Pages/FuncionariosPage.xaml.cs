using CommunityToolkit.Mvvm.ComponentModel;
using ServiceHub.Model;
using ServiceHub.ViewModel;
using System.Threading.Tasks;

namespace ServiceHub.View.Pages
{

    public partial class FuncionariosPage : ContentPage
    {
        public FuncionariosPage(Permission permissions)
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var userM = (User)e.SelectedItem;
            var user = VMfuncionarios.UserModels.Where(x => x.Id == userM.Id).FirstOrDefault();
            VMcontacts.VMUser = user;
            var page = new ContactPage();
            await Navigation.PushAsync(page);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewFuncionarioPage());
        }
    }
}
namespace ServiceHub.ViewModel
{
    public class VMfuncionarios : ObservableObject
    {
        RestService rest;
        public static List<UserModel> UserModels { get; set; }
        // lista de funcionarioa chefe
        private List<User> usersChefe;
        public List<User> UsersChefe
        {
            get => usersChefe;
            set => SetProperty(ref usersChefe, value);
        }

        // lista de funcionarios gerente
        private List<User> usersGerente;
        public List<User> UsersGerente
        {
            get => usersGerente;
            set => SetProperty(ref usersGerente, value);
        }

        // lista de funcionarios comuns
        private List<User> usersComum;
        public List<User> UsersComum
        {
            get => usersComum;
            set => SetProperty(ref usersComum, value);
        }

        public VMfuncionarios()
        {
            rest = new RestService();
            getUsers();
        }
        public void getUsers()
        {
            var Users = VMfuncionarios.UserModels;
            UsersComum = new();
            UsersGerente = new();
            UsersChefe = new();

            foreach (var user in Users)
            {
                if (user.LevelPermission == Permission.NV1)
                {
                    var userC = new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Office = user.Office,
                        image = user.Image
                    };
                    UsersComum.Add(userC);
                }
                if (user.LevelPermission == Permission.NV2)
                {
                    var userG = new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Office = user.Office,
                        image = user.Image
                    };
                    UsersGerente.Add(userG);
                }
                if (user.LevelPermission == Permission.NV3) 
                {
                    var userA = new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Office = user.Office,
                        image = user.Image
                    };
                    UsersChefe.Add(userA);
                }
            }
        }

        
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public byte[] image;
        public ImageSource ImagePerfil
        {
            get
            {
                if (image != null && image.Length > 0)
                {
                    return ImageSource.FromStream(() => new MemoryStream(image));
                }
                return null;
            }
        }
    }
}