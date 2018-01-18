using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        [Range(0, 99999)]
        public int VoteCount { get; set; }
    }
}
