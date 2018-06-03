using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.GameStoreApplication.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")]
        public string Password { get; set; }

        public bool IsAdmin{ get; set; }

        public List<UserGame> UserGames { get; set; } = new List<UserGame>();
    }
}
