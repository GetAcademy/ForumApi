using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Answer : IAuditable
    {
        public int AnswerId { get; set; }
        [Required]
        public string AnswerContent { get; set; }
        public int AnswerParent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
