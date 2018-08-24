using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.SeedDtoModels
{
    public class ProductDto
    {
        public string Title { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Gender { get; set; }
        
        public string Type { get; set; }
        
        public string Brand { get; set; }
        
        public string Description { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal Price { get; set; }

        public string TeamName { get; set; }
    }
}
