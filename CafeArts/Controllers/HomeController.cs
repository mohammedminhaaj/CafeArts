using CafeArts.Models;
using CafeArts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace CafeArts.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View("../Admin/Dashboard");
            }

            else
            {
                var random = new Random();
                ViewBag.Products = _context.Products.Include(p => p.Category).Where(m => m.IsFeatured);
                var RandomReviews = _context.Review.Include(m => m.prod).Include(m => m.ApplicationUsers).Where(m => m.Rating == "Like" && !(m.ReviewData == null));
                ViewBag.FirstReview = RandomReviews.ToList().ElementAt(random.Next(RandomReviews.Count()));
                ViewBag.SecondReview = RandomReviews.ToList().ElementAt(random.Next(RandomReviews.Count()));
                ViewBag.ThirdReview = RandomReviews.ToList().ElementAt(random.Next(RandomReviews.Count()));
                return View();
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}