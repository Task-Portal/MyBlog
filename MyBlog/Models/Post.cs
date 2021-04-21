using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Post : Base
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Post Titile")]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Post  Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(1024)]
        [Display(Name = "Post Content")]
        public string Content { get; set; }


        [MaxLength(100)]
        [Display(Name = "Post Image")]
        public string ImagePath { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
