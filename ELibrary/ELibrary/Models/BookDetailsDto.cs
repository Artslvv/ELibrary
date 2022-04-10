using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        [Range(0, 9999)]
        public int Price { get; set; }
        [Range(0,9999)]
        public int Purchases  { get; set; }
        [Range(0,100)]
        public int AgeLimit { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public bool Available { get; set; }

        public ICollection<UserDto> Users { get; set; }
    }
}