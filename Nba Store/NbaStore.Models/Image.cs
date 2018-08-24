using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
