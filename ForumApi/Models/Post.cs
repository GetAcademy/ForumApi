﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Post : IAuditable
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string CreatedBy { get; set; }
        public int Votes { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
