﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Answer : IAuditable
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
