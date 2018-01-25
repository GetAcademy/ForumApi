using System;

namespace ForumApi.Models
{
    public class Answer : IAuditable
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public int CategoryId { get; set; }
        public int PostId { get; set; }
        public string CreatedBy { get; set; }
        public int Votes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
