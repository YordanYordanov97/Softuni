﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Application.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
