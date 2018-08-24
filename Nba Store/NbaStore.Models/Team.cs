using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string LogoUrl { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
