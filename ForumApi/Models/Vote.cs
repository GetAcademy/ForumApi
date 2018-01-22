using System;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }
        public string VoteParent { get; set; } // answer | post
        public int VoteParentId { get; set; } // id of answer or post
        [Range(0, 999999)]
        public int VoteCount { get; set; }
    }
}
