using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Price { get; set; }
        
        [Required]
        public int Purchases  { get; set; }
        
        [Required]
        public int AgeLimit { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool Available { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}