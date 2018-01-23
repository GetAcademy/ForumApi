using System.ComponentModel.DataAnnotations;

namespace ForumApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
