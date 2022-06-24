using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Bll.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]        
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
