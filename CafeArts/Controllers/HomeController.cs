using CafeArts.Models;
using CafeArts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
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
                var FirstReview = await RandomReviews.ToListAsync();
                ViewBag.FirstReview = FirstReview.ElementAt(random.Next(await RandomReviews.CountAsync()));
                var SecondReview = await RandomReviews.ToListAsync();
                ViewBag.SecondReview = SecondReview.ElementAt(random.Next(await RandomReviews.CountAsync()));
                var ThirdReview = await RandomReviews.ToListAsync();
                ViewBag.ThirdReview = ThirdReview.ElementAt(random.Next(await RandomReviews.CountAsync()));
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
            return View();
        }

        public async Task<ActionResult> SaveFeedback(Feedback feedbackmodel)
        {
            if(!ModelState.IsValid)
            {
                return View("Contact", feedbackmodel);
            }
            else
            {
                feedbackmodel.CreatedDate = DateTime.Now;
                _context.Feedbacks.Add(feedbackmodel);
                await _context.SaveChangesAsync();
                TempData["FeedbackSuccess"] = "Query submitted successfully!";
                return RedirectToAction("Contact");
            }
            
        }
    }
}