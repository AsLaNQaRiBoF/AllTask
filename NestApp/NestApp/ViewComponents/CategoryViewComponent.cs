namespace NestApp.ViewComponents
{
    public class CategoryViewComponent
    {
        ViewComponent
    {
      private readonly AppDbContext _context;
        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {

            return View(await _context.categories.
                Where(x => x.IsDeleted == false).
                OrderByDescending(p => p.Products.Count).Take(take).ToListAsync());
        }

    }
}
