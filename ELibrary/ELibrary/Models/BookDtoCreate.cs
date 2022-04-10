using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class BookDtoCreate
    {
            public int Id { get; set; }
            [Range(0, 9999)]
            public int Price { get; set; }
            [Range(0,100)]
            public int AgeLimit { get; set; }
            [Required]
            [StringLength(100)]
            public string Name { get; set; }
            public bool Available { get; set; }
        
    }
}