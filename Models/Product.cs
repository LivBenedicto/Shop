using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    // depends on Category

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        // Category class
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; } // only the reference
        // and/or
        public Category Category { get; set; } // all details
            // allows the use of Includes()
    }
}