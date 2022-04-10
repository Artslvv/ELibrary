using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class UserDetailsDto
    {
        
        public int Id { get; set; }
        public int Role { get; set; }
        [Range(0,120)]
        public int Age { get; set; }
        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(100)]
        public string Lastname { get; set; }
        
        public ICollection<BookDto> Books { get; set; }
    }
}