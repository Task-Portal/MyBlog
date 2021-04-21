using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Comment : Base
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Comment Text")]
        public string Content { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}