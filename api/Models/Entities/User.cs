﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace api.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
