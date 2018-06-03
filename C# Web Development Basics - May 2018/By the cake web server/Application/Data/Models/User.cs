﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Application.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
