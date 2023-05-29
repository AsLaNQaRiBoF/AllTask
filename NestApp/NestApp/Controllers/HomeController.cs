using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestApp.DAL;
using NestApp.Models;
using NestApp.ViewModel;
using Newtonsoft.Json;
using HomeVN = NestApp.ViewModel.HomeVM;

namespace NestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);

            //}
            //      public async Task<IActionResult> Index()
            //      {
            //          Response.Cookies.Append("surname", "Qaribov", new CookieOptions { MaxAge=TimeSpan.FromSeconds(10) };
            //          HomeVM homeVM = new HomeVM();
            //          {
            //              Sliders = await _context.Sliders.ToListAsync(),
            //              PopularCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Products.Count).ToListAsync(),
            //              RandomCategories = await _context.Categories.Where(x => x.IsDeleted == false).OrderBy(x => Guid.NewGuid()).ToListAsync(),
            //              TopRatedProducts = await _context.Products.Include(x => x.ProductImages).Where(x => x.IsDeleted == false).OrderByDescending(p => p.Rating).Take(3).ToListAsync(),
            //              RecentProducts = await _context.Products.Include(x => x.ProductImages).Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
            //             }
            //          ;
            //          return View(homeVM);
            //      }
            //public IActionResult Test()
            //{
            //    var result = HttpContext.Request.Cookies["surname"];
            //    return View();
            //}
        }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddBasket(int? id)
            {
                if (id == null) return NotFound();
                Product? product = await _context.Products.FindAsync(id);
                if (product == null) return BadRequest();
                List<BasketVM> basket = GetBasket();

                UpdateBasket(product.Id, basket);
                return RedirectToAction("Index", "Home");
            }
            private List<BasketVM> GetBasket()
            {
                List<BasketVM> basket;
                if (Request.Cookies["basket"] != null)
                {
                    basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                }
                else basket = new List<BasketVM>();
                return basket;
            }
            private void UpdateBasket(int id, List<BasketVM> basket)
            {
                BasketVM basketVM = basket.Find(x => x.Id == id);

                if (basket.Any(x => x.Id == id))
                {
                    basketVM.Count++;
                }
                else
                {
                    basket.Add(new BasketVM
                    {
                        Id = id,
                        Count = 1
                    });
                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }

            public IActionResult DeleteBasketItem(int id)
            {
                List<BasketVM> basket = GetBasket();
                BasketVM basketVM = basket.Find(x => x.Id == id);
                basket.Remove(basketVM);
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
                return RedirectToAction(nameof(Cart));
            }
            public IActionResult Cart()
            {
                return View();
            }
        }
    } 

    

