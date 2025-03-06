using System;
using api.Models.Entities;

namespace api.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }

        public string UserNome { get; set; }
        public string UserEmail { get; set; }
        public int UserId { get; set; }
    }
}
