using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public Post Post { get; set; }
        public Answer Answer { get; set; }
        public User User { get; set; }
        [Range(0, 99999)]
        public int VoteCount { get; set; }
    }
}
