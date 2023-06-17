using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Souq.com.Data;
using Souq.com.Models;
using System.Diagnostics;

namespace Souq.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
        
            _logger = logger;
            _context = context; 
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            IndexVM result= new IndexVM();
            result.Categories= _context.Categories.ToList();
            result.Products= _context.Products.ToList();    
            result.Reviews= _context.Reviews.ToList();  

            return View(result);
        }

        public IActionResult Privacy()
        {
            var users= _userManager.Users.ToList(); 
            return View(users);
        }

        public IActionResult GetProductsByCategory(int id)
        {
            var products= _context.Products.Where(x=>x.CategoryId==id).ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult ProductSearch(string xname)
        {
            var products = _context.Products.Where(x=>x.Name.Contains(xname)).ToList();
            return View(products);
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SendReview(Review review)
        {
            _context.Reviews.Add(new Review
            {
                Name= review.Name,  
                Subject=review.Subject,
                Message=review.Message, 
                Email=review.Email, 
            });
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }

        public IActionResult Reviews()
        {
            var reviews = _context.Reviews.ToList();

            return View(reviews);

        }

        public IActionResult CurrentProduct(int id)
        {
            var product=_context.Products.Include(x=>x.Category).FirstOrDefault(x=>x.Id==id);
            return View(product);
        }

        [Authorize]
        public IActionResult AddProductToCart(int id)
        {
            var price = _context.Products.Find(id).Price;

            var item= _context.Carts.FirstOrDefault(x=>x.ProductId==id && x.UserId==User.Identity.Name);

            if (item!=null)
            {
                item.Qty += 1;
            }
            else
            {
                _context.Carts.Add(new Cart
                {
                    ProductId=id,
                    UserId=User.Identity.Name,
                    Qty=1,
                    Price=price,    
                });
            }
            _context.SaveChanges();


            return Redirect("~/Carts/Index");

        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            Order o = new Order
            {
                Email = order.Email,
                Address = order.Address,
                Name = order.Name,
                Phone = order.Phone,
                UserId = User.Identity.Name
            };

            var cartItem = _context.Carts.Where(x => x.UserId == User.Identity.Name).ToList();

            _context.Carts.RemoveRange(cartItem);
            _context.Orders.Add(o);
            _context.SaveChanges();

            return Redirect("~/Carts/Index");
        
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}