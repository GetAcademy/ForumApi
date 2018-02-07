using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApi.Models
{
    public class Vote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }
        public int CategoryId { get; set; } 
        public int PostId { get; set; }
        public int AnswerId { get; set; }
        public int VotedBy { get; set; }
    }
}
