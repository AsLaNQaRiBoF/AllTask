namespace NestApp.Models
{
	public class Slider
	{
		public int Id { get; set; }
		public string Title1 { get; set; }
		public string Title2 { get; set; }
		public string Image { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
