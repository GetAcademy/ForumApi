using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
