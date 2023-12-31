﻿using Firebase.Iid;
using Plugin.Firebase.CloudMessaging;
using ServiceHub.Model;
using ServiceHub.ViewModel;

namespace ServiceHub.View.Pages
{
    public partial class MainPage : ContentPage
    {
        readonly RestService Rest;
        public MainPage()
        {
            InitializeComponent();
            Rest = new RestService();
        }

        private async void LoginBT(object sender, EventArgs e)
        {
            var Verify = Rest.Login(cpf.Text, senha.Text);
            if (await Verify)
            {
                await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                if (refreshedToken == null)
                {
                    var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
                    Rest.UpdateToken(InicializeApp.User.Id, token);
                }
                else
                {
                    Rest.UpdateToken(InicializeApp.User.Id, refreshedToken);
                }
                Thread thread = new(getUsers);
                thread.IsBackground = true;
                thread.Start();
                await Shell.Current.GoToAsync("///HomePage");
            }
        }
        public async void getUsers()
        {
            VMfuncionarios.UserModels = await Rest.GetUsers();
        }
    }
}