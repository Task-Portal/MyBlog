using System;
using System.Collections.Generic;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class PageVM
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public PageVM(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling((double) count / pageSize);
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextpage => PageNumber < TotalPages;
    }

    public class PostVM
    {
        public List<Post> Posts { get; set; }
        public PageVM Paginator { get; set; }
    }
}
