using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;

namespace NestApp.ViewModels
{
    public class ProductWievComponent: ViewComponent
    
         {

    private readonly AppDbContext _context;

        public ProductWievComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            return View(await _context.Products.
                Include(x => x.Category).
                Include(x => x.ProductImages).Take(take).ToListAsync());
        }
    }
}
