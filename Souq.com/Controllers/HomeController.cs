using Microsoft.AspNetCore.Mvc;
using Souq.com.Data;
using Souq.com.Models;
using System.Diagnostics;

namespace Souq.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            var categories= _context.Categories.ToList();
            ViewBag.products= _context.Products.ToList();   
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
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




            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}