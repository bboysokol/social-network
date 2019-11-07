namespace SocialNetwork.Data.ViewModels
{
    public class CommentVM
    {
        public UserVM Author { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
    }
}
