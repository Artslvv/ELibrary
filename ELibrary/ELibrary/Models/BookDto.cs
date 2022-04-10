using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class BookDto
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}