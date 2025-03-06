using Microsoft.EntityFrameworkCore.Update.Internal;

namespace api.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
