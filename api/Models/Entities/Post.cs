using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public string Titulo { get; set; }

        //[Column(TypeName = "varbinary(MAX)")]
        public string Text { get; set; }
        public byte[] TextByte { get; set; }
        public virtual IEnumerable<Comment>? Comments { get; set; }

    }
}
