namespace WebAppMvc.Models
{
    public class PostMetaData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }    
        public int PostId { get; set; }
        public Post Post { get; set; }

        public DateTime CreatedDaate { get; set; }

    }
}
