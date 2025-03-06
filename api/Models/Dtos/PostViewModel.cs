using System;
using System.Collections;
using System.Collections.Generic;

namespace api.Models.Dtos
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Titulo { get; set; }
        public string Text { get; set; }

        public byte[]? image { get; set; }
        public byte[]? textByte { get; set; }

    }
}
