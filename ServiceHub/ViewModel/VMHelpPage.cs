using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMHelpPage : ObservableObject
    {
        private RestService Rest;

        private List<MessageModel> messages;
        public List<MessageModel> ChatGeral
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        private string _msg;
        public string Msg
        {
            get => _msg;
            set => SetProperty(ref _msg, value);
        }

        public IAsyncRelayCommand SendMSG { get; set; }

        public VMHelpPage()
        {
            Rest = new RestService();
            SendMSG = new AsyncRelayCommand(SendMessage);
            GetChat();
        }
        private async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(Msg))
            {
                var message = new MessageModel()
                {
                    User = InicializeApp.User.Name,
                    Message = Msg
                };
                await Rest.SendMessage(message);
            }
            Msg = string.Empty;
            await GetChat();
        }
        private async Task GetChat()
        {
            ChatGeral = await Rest.AllChat();
        }
    }
}
