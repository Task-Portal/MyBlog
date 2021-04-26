using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class PostVM
    {
        public List<Post> Posts { get; set; }
        public PageVM Paginator { get; set; }
        public SelectList Categories { get; set; }
    }
}