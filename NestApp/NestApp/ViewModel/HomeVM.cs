using NestApp.Models;

namespace NestApp.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Models.Category> PopularCategories { get; set; }
        public List<Models.Category> RandomCategories { get; set; }
        public List<Product> TopRatedProducts { get; set; }
        public List<Product> RecentProducts { get; set; }
    }
}
