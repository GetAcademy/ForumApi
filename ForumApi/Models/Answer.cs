using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Answer : IAuditable
    {
        [Key]
        public int AnswerId { get; set; }
        public int PostId { get; set; }
        public string AnswerContent { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
