using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace api.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string RefreshToken { get; set; }

        [Required(AllowEmptyStrings = true)]
        public DateTime? RefreshTokenEndDate { get; set; }
        //public virtual IEnumerable<Post> Posts { get; set; }
        //public virtual IEnumerable<Comment> Comment { get; set; }
    }
}
