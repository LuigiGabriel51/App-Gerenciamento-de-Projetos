using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using ServiceHub.Model;
using ServiceHub.ViewModel;
using System.Text;

namespace ServiceHub
{
    public class RestService
    {
        private HttpClient _httpClient;
        private const string _url = "https://ql33c2kw-7085.brs.devtunnels.ms/";
        public RestService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> Login(string CPF, string Password)
        {
            try
            {
                Uri uri = new Uri($"{_url}Login?CPF={CPF}&Password={Password}");
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserModel>(content);

                    InicializeApp.User = user;
                    if (InicializeApp.User.LevelPermission == Permission.NV1) InicializeApp.permission = false;
                    else InicializeApp.permission = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async void UpdateToken(int id, string token)
        {
            Uri uri = new Uri($"{_url}UpdateTK?id={id}&token={token}");
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task<bool> SendMessage(MessageModel message)
        {
            try
            {
                Uri uri = new Uri($"{_url}SendMessage");

                string jsonMessage = JsonConvert.SerializeObject(message);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateImage(UserModel user)
        {
            Uri uri = new Uri($"{_url}User");
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var User = JsonConvert.DeserializeObject<UserModel>(strcontent);

                    InicializeApp.User = User;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<MessageModel>> AllChat()
        {
            Uri uri = new($"{_url}Message");
            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Messages = JsonConvert.DeserializeObject<List<MessageModel>>(content);
                    return Messages;
                }
                return null;
            }
            catch
            {
                Toast toast = new()
                {
                    Text = "Falha ao carregar Chat"
                };
                await toast.Show();
                return null;
            }
        }

        public async Task<List<Project>> GetProjects(string nome)
        {
            Uri uri = new($"{_url}Projects?nome={nome}");
            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ProjectsUser = JsonConvert.DeserializeObject<List<Project>>(content);
                    return ProjectsUser;
                }
                return null;
            }
            catch
            {
                Toast toast = new()
                {
                    Text = "Falha ao carregar Projetos"
                };
                await toast.Show();
                return null;
            }
        }

        public async Task<bool> UpdateProject(TaskModel project)
        {
            Uri uri = new Uri($"{_url}Projects");

            try
            {
                string jsonMessage = JsonConvert.SerializeObject(project);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var Project = JsonConvert.DeserializeObject<TaskModel>(strcontent);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> AllUsers()
        {
            Uri uri = new Uri($"{_url}User");
            try
            {
                var response = _httpClient.GetAsync(uri).Result; // Espere pelo resultado síncrono
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var usersAll = JsonConvert.DeserializeObject<List<UserModel>>(content);
                    List<string> users = new();
                    foreach (var user in usersAll)
                    {
                        users.Add(user.Name);
                    }
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddProject(TaskModel project)
        {
            Uri uri = new Uri($"{_url}Projects");
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(project);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var Project = JsonConvert.DeserializeObject<TaskModel>(strcontent);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStage(StageModel project)
        {
            Uri uri = new Uri($"{_url}PutStage");

            try
            {
                string jsonMessage = JsonConvert.SerializeObject(project);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var Project = JsonConvert.DeserializeObject<TaskModel>(strcontent);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddStage(StageModel project)
        {
            Uri uri = new Uri($"{_url}AddStage");
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(project);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var Project = JsonConvert.DeserializeObject<TaskModel>(strcontent);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DelStage(StageModel project)
        {
            Uri uri = new Uri($"{_url}DeleteStage?id={project.Id}");
            try
            {
                string jsonMessage = JsonConvert.SerializeObject(project);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                var response = await _httpClient.DeleteAsync(uri.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var strcontent = await response.Content.ReadAsStringAsync();
                    var Project = JsonConvert.DeserializeObject<TaskModel>(strcontent);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserModel>> GetUsers()
        {
            Uri uri = new($"{_url}User");
            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ProjectsUser = JsonConvert.DeserializeObject<List<UserModel>>(content);
                    return ProjectsUser;
                }
                return null;
            }
            catch
            {
                Toast toast = new()
                {
                    Text = "Falha ao carregar usuários!"
                };
                await toast.Show();
                return null;
            }
        }
    }
}