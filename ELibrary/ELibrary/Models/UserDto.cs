using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class UserDto
    {
        
        public int Id { get; set; }
        [Range(0,120)]
        public int Age { get; set; }
        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(100)]
        public string Lastname { get; set; }
    }
}