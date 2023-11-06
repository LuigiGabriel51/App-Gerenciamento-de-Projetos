using Plugin.Firebase.CloudMessaging;

namespace ServiceHub.Model
{
    public class InicializeApp
    {
        public static DataBaseUser db;

        public InicializeApp()
        {
            db = new DataBaseUser();
        }
        public static bool permission {  get; set; }
        public static UserModel User { get; set; }

        public static UserModel GetUser()
        {
            UserModel user = db.List().FirstOrDefault();
            if (user == null) { return null; }
            return user;
        }
        public static async void VerifyTokenMessaging(UserModel user)
        {
            if (user.ExpireToken < DateTime.UtcNow)
            {
                await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
                var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
                user.Token = token;
                user.ExpireToken = DateTime.UtcNow.AddDays(5);
                db.Update(user);
            }
        }
    }
}
