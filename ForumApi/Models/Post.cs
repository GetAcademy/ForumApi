using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Post : IAuditable
    {
        public int PostId { get; set; }
        [Required]
        public int PostParent { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
