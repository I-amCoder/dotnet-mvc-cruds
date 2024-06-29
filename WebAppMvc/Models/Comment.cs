namespace WebAppMvc.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content {  get; set; }

        public DateTime CreatedDate { get; set; }

        public Post Post { get; set; }
    }
}
