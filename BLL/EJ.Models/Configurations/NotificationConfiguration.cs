namespace EJ.Models.Configurations
{
    public class NotificationConfiguration
    {
        public NotificationEmailConfiguration Email { get; set; }

        public bool IsNotificationsEnabled { get; set; }
    }

    public class NotificationEmailConfiguration
    {
        public string From { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}