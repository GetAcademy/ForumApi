using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class AnswerDetail
    {
        [Key]
        public int AnswerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Url { get; set; }
    }
}
