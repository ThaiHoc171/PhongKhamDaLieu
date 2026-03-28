using FirebaseAdmin.Messaging;
namespace Infrastructure.Services
{
    public class FcmService
    {
        public async Task SendAsync(
            string fcmToken,
            string title,
            string body,
            Dictionary<string, string>? data = null)
        {
            var message = new Message
            {
                Token = fcmToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Data = data ?? new Dictionary<string, string>(),
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        ChannelId = "phongkham_channel",
                        Sound = "default"
                    }
                }
            };

            try
            {
                var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine($"FCM sent: {result}");
            }
            catch (FirebaseMessagingException ex)
            {
                Console.WriteLine($"FCM error [{ex.ErrorCode}]: {ex.Message}");
            }
        }
    }
}