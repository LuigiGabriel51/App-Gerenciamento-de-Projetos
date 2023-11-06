using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.ViewModel
{
    class VMcontacts: ObservableObject
    {
        public static UserModel VMUser {  get; set; }
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
        public VMcontacts()
        {
            this.User = VMcontacts.VMUser;
            Image = User.Image;
        }
    }
}
