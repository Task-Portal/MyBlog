using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Comment> Comments { get; set; }
    }
}