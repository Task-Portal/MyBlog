﻿using System.Collections.Generic;

namespace MyBlog.Models
{
    public class Category
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}