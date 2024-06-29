namespace WebAppMvc.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PostMetaData MetaData { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
