using CafeArts.Models;
using CafeArts.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeArts.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Categories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }


        public ActionResult AddCategory()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCategory([Bind(Exclude ="CategoryID")]Category category)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new Category()
                {
                    CategoryName = category.CategoryName
                };
                return View("AddCategory", ViewModel);
            }

            else
            {
                
                _context.Categories.Add(category);

                _context.SaveChanges();
        
                return RedirectToAction("Categories", "Admin");

            }
        }

        public ActionResult ModifyCategory(int id)
        {
            var Cat = _context.Categories.Single(m => m.CategoryID == id);
            if (Cat == null)
                return HttpNotFound();

            var ViewModel = new Category
            {
                CategoryID = Cat.CategoryID,
                CategoryName = Cat.CategoryName
            };

            return View("ModifyCategory", ViewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new Category()
                {
                    CategoryName = category.CategoryName
                };
                return View("ModifyCategory", ViewModel);
            }

            else
            {

                var CategoryInDB = _context.Categories.Single(m => m.CategoryID == category.CategoryID);
                if (CategoryInDB == null)
                    return HttpNotFound();

                else
                {

                    CategoryInDB.CategoryName = category.CategoryName;

                    _context.SaveChanges();

                    return RedirectToAction("Categories", "Admin");

                }

            }


        }


        //public ActionResult DeleteCategory(int id)
        //{
        //    var Cat = _context.Categories.Single(m => m.CategoryID == id);

        //    if (Cat == null)
        //        return HttpNotFound();

        //    else
        //    {
        //        _context.Categories.Remove(Cat);
        //        _context.SaveChanges();
        //        return RedirectToAction("Categories", "Admin");
        //    }
        //}

        [HttpGet]
        public ActionResult Products()
        {
            
            var products = _context.Products.ToList();
            
            return View(products);
        }

        
        public ActionResult AddProduct()
        {
            var Cat = _context.Categories.ToList();
            var ViewModel = new ProductCategories
            { Categories = Cat };
            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateProduct(ProductCategories Prod, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
        {

            if (!ModelState.IsValid)
            {
                var ViewModel = new ProductCategories
                {
                    Products = Prod.Products,
                    Categories = _context.Categories.ToList()
                };
                return View("AddProduct", ViewModel);
            }

            else
            {
                if ((Image1 != null && Image1.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image1.InputStream))
                    {
                        Prod.Products.ProductImage1 = reader.ReadBytes(Image1.ContentLength);
                    }
                }

                if ((Image2 != null && Image2.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image2.InputStream))
                    {
                        Prod.Products.ProductImage2 = reader.ReadBytes(Image2.ContentLength);
                    }
                }

                if ((Image3 != null && Image3.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image3.InputStream))
                    {
                        Prod.Products.ProductImage3 = reader.ReadBytes(Image3.ContentLength);
                    }
                }

                if ((Image4 != null && Image4.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image4.InputStream))
                    {
                        Prod.Products.ProductImage4 = reader.ReadBytes(Image4.ContentLength);
                    }
                }

                Prod.Products.CreatedDate = DateTime.Now;

                _context.Products.Add(Prod.Products);

                _context.SaveChanges();

                return RedirectToAction("Products", "Admin");

            }
        }

        public ActionResult ModifyProduct(int id)
        {
            var prod = _context.Products.Single(m => m.Id == id);
            if (prod == null)
                return HttpNotFound();

            var ViewModel = new ProductCategories
            {
                Products = prod,
                Categories = _context.Categories.ToList()
            };

            return View("ModifyProduct", ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateProduct(ProductCategories Prod, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new ProductCategories
                {
                    Products = Prod.Products,
                    Categories = _context.Categories.ToList()
                };
                return View("ModifyProduct", ViewModel);
            }

            else
            {
                if ((Image1 != null && Image1.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image1.InputStream))
                    {
                        Prod.Products.ProductImage1 = reader.ReadBytes(Image1.ContentLength);
                    }
                }

                if ((Image2 != null && Image2.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image2.InputStream))
                    {
                        Prod.Products.ProductImage2 = reader.ReadBytes(Image2.ContentLength);
                    }
                }

                if ((Image3 != null && Image3.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image3.InputStream))
                    {
                        Prod.Products.ProductImage3 = reader.ReadBytes(Image3.ContentLength);
                    }
                }

                if ((Image4 != null && Image4.ContentLength > 0))
                {
                    using (var reader = new System.IO.BinaryReader(Image4.InputStream))
                    {
                        Prod.Products.ProductImage4 = reader.ReadBytes(Image4.ContentLength);
                    }
                }

                var ProductInDB = _context.Products.Single(m => m.Id == Prod.Products.Id);
                if (ProductInDB == null)
                    return HttpNotFound();
                else
                {

                    ProductInDB.IsActive = Prod.Products.IsActive;
                    ProductInDB.IsFeatured = Prod.Products.IsFeatured;
                    ProductInDB.ModifiedDate = DateTime.Now;
                    ProductInDB.ProductDescription = Prod.Products.ProductDescription;

                    if (Prod.Products.ProductImage1!= null)
                        ProductInDB.ProductImage1 = Prod.Products.ProductImage1;
                    if (Prod.Products.ProductImage2 != null)
                        ProductInDB.ProductImage2 = Prod.Products.ProductImage2;
                    if (Prod.Products.ProductImage3 != null)
                        ProductInDB.ProductImage3 = Prod.Products.ProductImage3;
                    if (Prod.Products.ProductImage4 != null)
                        ProductInDB.ProductImage4 = Prod.Products.ProductImage4;

                    ProductInDB.ProductName = Prod.Products.ProductName;
                    ProductInDB.ProductPrice = Prod.Products.ProductPrice;
                    ProductInDB.ProductQuantity = Prod.Products.ProductQuantity;
                    ProductInDB.RibbonStatus = Prod.Products.RibbonStatus;
                    ProductInDB.CategoryID = Prod.Products.CategoryID;


                    _context.SaveChanges();

                    return RedirectToAction("Products", "Admin");

                }

            }


                
        }


        //[ValidateAntiForgeryToken]
        //[HttpPost]    
        //public ActionResult DeleteProduct([Bind(Include ="Id, IsActive")]ProductModels visible_product)
        //{
        //    var ProductInDB = _context.Products.Single(m => m.Id == visible_product.Id);
            

        //    if (ProductInDB == null)
        //        return HttpNotFound();

        //    else
        //    {
        //        if(ProductInDB.IsActive)
        //            ProductInDB.IsActive = false;
        //        else
        //            ProductInDB.IsActive = true;

        //        _context.SaveChanges();
        //        return RedirectToAction("Products", "Admin");
        //    }
        //}

        public ActionResult Orders()
        {
            var Orders = _context.orders.Where(m => m.OrderStatus == "Ordered");
            return View(Orders);
        }

        
        public ActionResult ConfirmOrder(int id)
        {
            var OrderInDB = _context.orders.Single(m => m.OrderID == id);
            OrderInDB.OrderStatus = "Confirmed";
            _context.SaveChanges();
            return RedirectToAction("Orders","Admin");
        }

        public ActionResult ConfirmedOrders()
        {
            return View("Orders", _context.orders.Where(m => m.OrderStatus == "Confirmed"));
        }

        public ActionResult OrderDetails(int id)
        {
            var CartModel = _context.Carts.Include(m => m.Product).Where(m => m.OrderID == id && !m.IsActive);
            ViewBag.SingleOrderDetails = _context.orders.Single(m => m.OrderID == id);
            return View(CartModel);
        }

        public ActionResult CheckAsDelivered(int id)
        {
            var OrderInDB = _context.orders.Single(m => m.OrderID == id);
            OrderInDB.OrderStatus = "Delivered";
            _context.SaveChanges();
            return RedirectToAction("PostConfirmationOrders", "Admin");
        }

        public ActionResult PostConfirmationOrders()
        {
            return View("Orders", _context.orders.Where(m => m.OrderStatus == "Delivered" || m.OrderStatus == "Canceled" || m.OrderStatus == "In transit"));
        }

        public ActionResult CancelOrder(int id)
        {
            var OrderInDB = _context.orders.Single(m => m.OrderID == id);
            OrderInDB.OrderStatus = "Canceled";
            _context.SaveChanges();
            return RedirectToAction("PostConfirmationOrders", "Admin");
        }

        public ActionResult AddWayBill(int id, string waybill)
        {
            var OrderInDB = _context.orders.Single(m => m.OrderID == id);
            if (waybill == "")
            {
                TempData["ErrorForWayBill"] = "Cannot submit empty waybill!";
                return RedirectToAction("OrderDetails","Admin", new { id = OrderInDB.OrderID});

            }
            else
            {
                OrderInDB.WayBill = waybill;
                OrderInDB.OrderStatus = "In transit";
                _context.SaveChanges();
                return RedirectToAction("OrderDetails", "Admin", new { id = OrderInDB.OrderID });
            }

        }

        public ActionResult CustomizeRequests()
        {
            var CustomizeModel = _context.Customizing.Include(m => m.Categories).ToList();
            return View(CustomizeModel);
        }

        public ActionResult DeleteCustomize(int id)
        {
            _context.Customizing.Remove(_context.Customizing.Single(m => m.CustomizeID == id));
            _context.SaveChanges();
            return RedirectToAction("CustomizeRequests");
        }

        public ActionResult Queries()
        {
            var QueryModel = _context.Feedbacks.ToList();
            return View(QueryModel);
        }
    }
}