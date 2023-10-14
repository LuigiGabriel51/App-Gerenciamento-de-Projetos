using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMUserPage : ObservableObject
    {
        private RestService rest;
        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        private byte[] _image;
        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        public IAsyncRelayCommand UpdateImage { get; set; }
        public VMUserPage()
        {
            this.User = InicializeApp.User;
            this.rest = new RestService();
            Image = User.Image;
            UpdateImage = new AsyncRelayCommand(UpdateIMG);
        }
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
                var user = InicializeApp.User;
                user.Image = byteArray;
                if (await rest.UpdateImage(user))
                {
                    Toast toast = new Toast()
                    {
                        Text = "Imagem atualizada!"
                    };
                    await toast.Show();
                    User = InicializeApp.User;
                    Image = User.Image;
                }
                else
                {
                    Toast toast = new Toast()
                    {
                        Text = "Falha ao atualizar imagem!"
                    };
                    await toast.Show();
                }

            }
        }

    }
}
