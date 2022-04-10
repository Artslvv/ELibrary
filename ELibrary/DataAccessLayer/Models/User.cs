using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Role { get; set; }
        
        [Required]
        public int Age { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Login { get; set; }
        
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Lastname { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}