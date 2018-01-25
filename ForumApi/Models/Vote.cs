namespace ForumApi.Models
{
    public class Vote
    {
        public int CategoryId { get; set; }
        public int VoteId { get; set; }
        public int PostId { get; set; }
        public int AnswerId { get; set; }
        public int VoteCount { get; set; }
        public int VotedBy { get; set; }
    }
}
