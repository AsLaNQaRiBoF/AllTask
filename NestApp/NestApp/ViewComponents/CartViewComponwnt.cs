using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;
using NestApp.ViewModel;
using Newtonsoft.Json;

namespace NestApp.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public CartViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM>? basket = GetBasket();

            List<BasketItemVM> basketItems = new List<BasketItemVM>();
            foreach (var item in basket)
            {
                var products = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == item.Id);
                basketItems.Add(new BasketItemVM
                {
                    Id = products.Id,
                    Count = item.Count,
                    Name = products.Name,
                    Image = products.ProductImages.FirstOrDefault(x => x.IsFront == true).Image,
                    SellPrice = products.SellPrice,
                    Rating = products.Rating,
                });
            }

            return View(basketItems);
        }

        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket = new List<BasketVM>();
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            return basket;
        }

    }
}
