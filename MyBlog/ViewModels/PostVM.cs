using System.Collections.Generic;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class PostVM
    {
        public List<Post> Posts { get; set; }
        public PageVM Paginator { get; set; }
    }
}