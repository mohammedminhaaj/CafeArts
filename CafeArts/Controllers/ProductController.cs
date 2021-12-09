using CafeArts.Models;
using CafeArts.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CafeArts.Controllers
{

    public class ProductController : Controller
    {

        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        // GET: Product
        [HttpGet]
        public ActionResult All(string search)
        {
            var products = _context.Products.ToList();

            ViewBag.SearchResult = "No products found for the search '" + search + "'";

            if (search != null)
            {
                
                return View(products.Where(m => m.IsActive && (m.ProductDescription.Contains(search) || m.ProductName.Contains(search))));
            }

            else
            {
                return View(products.Where(m => m.IsActive));
            }
            
        }

        public ActionResult Clocks()
        {
            var products = _context.Products.ToList();
            return View("All",products.Where(m => m.IsActive && m.CategoryID == 46));
        }

        public ActionResult Coasters()
        {
            var products = _context.Products.ToList();
            return View("All",products.Where(m => m.IsActive && m.CategoryID == 47));
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                var ViewModel = new ProductReviews() { Prods = _context.Products.Single(m => m.Id == id) };
                ViewBag.ReviewList = _context.Review.Include(m => m.ApplicationUsers).ToList().OrderByDescending(m => m.ReviewId);
                
                return View(ViewModel);
            }

            catch(Exception)
            {
                return HttpNotFound();
            }

            
        }

        [PreventFromUrl]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostReview(ProductReviews rev)
        {
            if (rev.Revs.ReviewData==null && rev.Revs.Rating==null)
            {
                return RedirectToAction("ProductDetails", new { id = rev.Prods.Id });
            }
            else
            {
                rev.Revs.IsValid = true;
                rev.Revs.ReviewCreatedDate = DateTime.Now;
                rev.Revs.ProdID = rev.Prods.Id;
                rev.Revs.MemberID = User.Identity.GetUserId();
                _context.Review.Add(rev.Revs);
                _context.SaveChanges();
                return RedirectToAction("ProductDetails", new { id = rev.Prods.Id });
            }

        }


        [PreventFromUrl]
        public ActionResult DeleteReview(int id)
        {
            var ReviewInDB = _context.Review.Single(m => m.ReviewId == id);

            if (ReviewInDB == null)
                return HttpNotFound();

            ReviewInDB.IsValid = false;
            _context.SaveChanges();

            return RedirectToAction("ProductDetails", new { id = ReviewInDB.ProdID });
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}