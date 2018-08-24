using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Models
{
    public class User:IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FullName{ get; set; }

        public DateTime DateOfRegistration { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
