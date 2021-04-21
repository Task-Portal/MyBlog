using System;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Base
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Publish Time")]
        public DateTime PublishTime { get; set; }
    }
}