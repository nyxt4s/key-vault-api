using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Application.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public BusinessDto Business { get; set; }
        public BrandDto Brand { get; set; }
        public CategoryDto Category { get; set; }
    }

    public class BusinessDto
    {
        public int BusinessID { get; set; }
        public string Name { get; set; }
    }

    public class BrandDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}


