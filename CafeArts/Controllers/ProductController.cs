using CafeArts.Models;
using CafeArts.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> All(string search)
        {
            var products = await _context.Products.ToListAsync();

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

        public async Task<ActionResult> Clocks()
        {
            var products = await _context.Products.ToListAsync();
            return View("All",products.Where(m => m.IsActive && m.CategoryID == 46));
        }

        public async Task<ActionResult> Coasters()
        {
            var products = await _context.Products.ToListAsync();
            return View("All",products.Where(m => m.IsActive && m.CategoryID == 47));
        }

        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                var ViewModel = new ProductReviews() { Prods = await _context.Products.SingleAsync(m => m.Id == id) };
                var ReviewList = await _context.Review.Include(m => m.ApplicationUsers).ToListAsync();
                ViewBag.ReviewList = ReviewList.OrderByDescending(m => m.ReviewId);
                
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
        public async Task<ActionResult> PostReview(ProductReviews rev)
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
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductDetails", new { id = rev.Prods.Id });
            }

        }


        [PreventFromUrl]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var ReviewInDB = await _context.Review.SingleAsync(m => m.ReviewId == id);

            if (ReviewInDB == null)
                return HttpNotFound();

            ReviewInDB.IsValid = false;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProductDetails", new { id = ReviewInDB.ProdID });
        }

        
        public async Task<ActionResult> CustomizeProduct()
        {
            var CustomModel = new Customize() { CategoriesForCustom = await _context.Categories.ToListAsync() };
            return View(CustomModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubmitCustomize(Customize customizeModel)
        {

            if (!ModelState.IsValid)
            {
                customizeModel.CategoriesForCustom = await _context.Categories.ToListAsync();
                return View("CustomizeProduct",customizeModel);
            }

            else
            {
                var CurrentUser = User.Identity.GetUserId();
                var usermodel = await _context.Users.SingleAsync(m => m.Id == CurrentUser);
                customizeModel.Email = usermodel.Email;
                customizeModel.FullName = usermodel.FirstName + " " + usermodel.LastName;
                customizeModel.CreatedDate = DateTime.Now;
                customizeModel.IsActive = true;
                _context.Customizing.Add(customizeModel);
                await _context.SaveChangesAsync();                
                return RedirectToAction("RequestSuccess","Product", new { value = customizeModel.ContactNumber});
            }
            
        }

        [PreventFromUrl]
        public ActionResult RequestSuccess(string value)
        {
            ViewBag.CustomizePhoneNumber = value;
            return View();
        }

    }
}