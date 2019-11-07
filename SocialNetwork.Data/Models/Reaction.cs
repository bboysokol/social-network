namespace SocialNetwork.Data.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
