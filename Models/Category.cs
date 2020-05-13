using System.ComponentModel.DataAnnotations;
// .schema : name table, column, data type etc

namespace Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public string Title { get; set; }
    }
}