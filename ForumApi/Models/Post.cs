using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Post : IAuditable
    {
        [Key]
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
