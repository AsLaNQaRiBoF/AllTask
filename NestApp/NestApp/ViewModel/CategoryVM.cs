using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NestApp.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [MaxLength(100), MinLength(2)]
        public string Name { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}
