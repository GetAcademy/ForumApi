using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class PostDetail
    {
        [Key]
        public int PostId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Url { get; set; }
    }
}
