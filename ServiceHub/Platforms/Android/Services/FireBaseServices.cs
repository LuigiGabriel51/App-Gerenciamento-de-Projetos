using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using Firebase.Messaging;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.CloudMessaging.Platforms.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Platforms.Android.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.Data != null && message.Data.Count > 0)
            {
                // Aqui você pode processar os dados da notificação recebida.
                // Por exemplo, exibir uma notificação.
                ShowNotification(message.Data);
            }
        }

        private void ShowNotification(IDictionary<string, string> data)
        {
            string title = data["title"];
            string message = data["message"];

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            // Crie um objeto de notificação.
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, "your_channel_id")
                .SetSmallIcon(Resource.Drawable.dotnet_bot) // Ícone da notificação
                .SetContentTitle(title) // Título da notificação
                .SetContentText(message) // Texto da notificação
                .SetAutoCancel(true) // Fechar a notificação ao ser tocada
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification)) // Som da notificação
                .SetContentIntent(pendingIntent);

            // Obtenha o NotificationManager para mostrar a notificação.
            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            // Verifique se a versão do Android é maior ou igual a Oreo para criar o canal de notificação.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel channel = new NotificationChannel($"{PackageName}.general", "General", NotificationImportance.Default);
                notificationManager.CreateNotificationChannel(channel);
            }

            // Mostre a notificação.
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}
