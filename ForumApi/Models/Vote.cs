using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        [Required]
        public int VoteParent { get; set; }
        [Range(0, 999999)]
        public int VoteCount { get; set; }
    }
}
