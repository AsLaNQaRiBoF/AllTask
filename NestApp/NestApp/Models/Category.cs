using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NestApp.Models
{
	public class Category
	{
		public int Id { get; set; }
		[MaxLength(100), MinLength(3)]
		public string Name { get; set; } = null!;
		public string? Logo { get; set; }
		public string? Photo { get; set; } 
		[NotMapped]
		public IFormFile PhotoFile { get; set; } = null!;
		public bool IsDeleted { get; set; }
		public ICollection<Product>? Products { get; set; }

        public List<Category> RandomCategories { get; set; }
        public List<Category> PopularCategories { get; set; }
    }
}
