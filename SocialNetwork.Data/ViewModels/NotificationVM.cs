namespace SocialNetwork.Api.Requests.NotificationVMs
{
    public class NotificationVM
    {
        public int Id { get; set; }
        public string CreateTime { get; set; }
        public string Content { get; set; }
        public bool IsReaded { get; set; }
    }
}
