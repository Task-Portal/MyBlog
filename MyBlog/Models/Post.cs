using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

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

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Publish Time")]
        public DateTime PublishTime { get; set; }

        [MaxLength(100)]
        [Display(Name = "Post Image")]
        public string ImagePath { get; set; }

        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
    }
}
