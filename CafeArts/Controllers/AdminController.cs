using CafeArts.Models;
using CafeArts.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CafeArts.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            _context.Dispose();
        }
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }


        public ActionResult AddCategory()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateCategory([Bind(Exclude ="CategoryID")]Category category)
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

                await _context.SaveChangesAsync();
        
                return RedirectToAction("Categories", "Admin");

            }
        }

        public async Task<ActionResult> ModifyCategory(int id)
        {
            var Cat = await _context.Categories.SingleAsync(m => m.CategoryID == id);
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
        public async Task<ActionResult> UpdateCategory(Category category)
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

                var CategoryInDB = await _context.Categories.SingleAsync(m => m.CategoryID == category.CategoryID);
                if (CategoryInDB == null)
                    return HttpNotFound();

                else
                {

                    CategoryInDB.CategoryName = category.CategoryName;

                    await _context.SaveChangesAsync();

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
        public async Task<ActionResult> Products()
        {
            
            var products = await _context.Products.ToListAsync();
            
            return View(products);
        }

        
        public async Task<ActionResult> AddProduct()
        {
            var Cat = await _context.Categories.ToListAsync();
            var ViewModel = new ProductCategories
            { Categories = Cat };
            return View(ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCategories Prod, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
        {

            if (!ModelState.IsValid)
            {
                var ViewModel = new ProductCategories
                {
                    Products = Prod.Products,
                    Categories = await _context.Categories.ToListAsync()
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

                await _context.SaveChangesAsync();

                return RedirectToAction("Products", "Admin");

            }
        }

        public async Task<ActionResult> ModifyProduct(int id)
        {
            var prod = await _context.Products.SingleAsync(m => m.Id == id);
            if (prod == null)
                return HttpNotFound();

            var ViewModel = new ProductCategories
            {
                Products = prod,
                Categories = await _context.Categories.ToListAsync()
            };

            return View("ModifyProduct", ViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(ProductCategories Prod, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new ProductCategories
                {
                    Products = Prod.Products,
                    Categories = await _context.Categories.ToListAsync()
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

                var ProductInDB = await _context.Products.SingleAsync(m => m.Id == Prod.Products.Id);
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


                    await _context.SaveChangesAsync();

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

        
        public async Task<ActionResult> ConfirmOrder(int id)
        {
            var OrderInDB = await _context.orders.SingleAsync(m => m.OrderID == id);
            if (OrderInDB == null)
                return HttpNotFound();

            OrderInDB.OrderStatus = "Confirmed";
            await _context.SaveChangesAsync();
            return RedirectToAction("Orders","Admin");
        }

        public ActionResult ConfirmedOrders()
        {
            return View("Orders", _context.orders.Where(m => m.OrderStatus == "Confirmed"));
        }

        public async Task<ActionResult> OrderDetails(int id)
        {
            var CartModel = _context.Carts.Include(m => m.Product).Where(m => m.OrderID == id && !m.IsActive);
            ViewBag.SingleOrderDetails = await _context.orders.SingleAsync(m => m.OrderID == id);
            if (ViewBag.SingleOrderDetails == null)
                return HttpNotFound();
            return View(CartModel);
        }

        public async Task<ActionResult> CheckAsDelivered(int id)
        {
            var OrderInDB = await _context.orders.SingleAsync(m => m.OrderID == id);

            if (OrderInDB == null)
                return HttpNotFound();

            OrderInDB.OrderStatus = "Delivered";
            OrderInDB.DeliveryDate = DateTime.Now;
            await _context.SaveChangesAsync();

            var callbackUrl = Url.Action("Index", "Home","", protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(OrderInDB.MemberID, "Order confirmation", "<!DOCTYPE html><html><head><style>*{font-family:\"Raleway\", \"Helvetica Neue\", Helvetica, Arial, sans-serif;}.block{box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23);padding: 10px;}</style></head><body><div class=\"block\"><h1>Thank you for shopping with us.</h1><p>We have delivered your product(s) at the mentioned address. We hope to see you soon.</p><p>Please take a few seconds to leave a review for the product(s) you ordered.</p><p>Visit us again at <a href=\"" + callbackUrl + "\">Cafe Arts</a></p><p><small>Thanks and regards,<br>Team Cafe Arts</small></p></div></body></html>");

            return RedirectToAction("PostConfirmationOrders", "Admin");
        }

        public ActionResult PostConfirmationOrders()
        {
            return View("Orders", _context.orders.Where(m => m.OrderStatus == "Delivered" || m.OrderStatus == "Canceled" || m.OrderStatus == "In transit"));
        }

        public async Task<ActionResult> CancelOrder(int id)
        {
            var OrderInDB = await _context.orders.SingleAsync(m => m.OrderID == id);

            if (OrderInDB == null)
                return HttpNotFound();

            OrderInDB.OrderStatus = "Canceled";
            await _context.SaveChangesAsync();
            return RedirectToAction("PostConfirmationOrders", "Admin");
        }

        public async Task<ActionResult> AddWayBill(int id, string waybill)
        {
            var OrderInDB = await _context.orders.SingleAsync(m => m.OrderID == id);

            if (OrderInDB == null)
                return HttpNotFound();

            if (waybill == "")
            {
                TempData["ErrorForWayBill"] = "Cannot submit empty waybill!";
                return RedirectToAction("OrderDetails","Admin", new { id = OrderInDB.OrderID});

            }

            else
            {
                OrderInDB.WayBill = waybill;
                OrderInDB.OrderStatus = "In transit";
                await _context.SaveChangesAsync();
                return RedirectToAction("OrderDetails", "Admin", new { id = OrderInDB.OrderID });
            }

        }

        public async Task<ActionResult> ModifyOrder(int? id)
        {
            if (id == null)
            {
                ViewBag.Header = "Add Order";
                return View();
            }
                
            else
            {
                ViewBag.Header = "Edit Order";
                var OrderModel = await _context.orders.SingleAsync(m => m.OrderID == id);
                if (OrderModel == null)
                    return HttpNotFound();

                return View(OrderModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostOrder(Order ordermodel)
        {
            if(!ModelState.IsValid)
            {
                return View("ModifyOrder", ordermodel);
            }

            else
            {
                if(ordermodel.OrderID == null)
                {
                    ordermodel.OrderStatus = "Ordered";
                    ordermodel.CreatedDate = DateTime.Now;
                    ordermodel.WayBill = "NA";
                    var user = await UserManager.FindByNameAsync(ordermodel.CustomerEmail);
                    ordermodel.MemberID = user.Id;
                    if (ordermodel.OrderType == "Cash on delivery")
                    {
                        ordermodel.RazorPayKey = "Not available";
                        ordermodel.RPUniquePaymentID = "Not available";
                        ordermodel.TransactionID = "Not available";
                    }

                    if(ordermodel.IsCustomized)
                    {
                        var DeleteCustomRequest = await  _context.Customizing.Where(m => m.Email == ordermodel.CustomerEmail).OrderByDescending(m => m.CustomizeID).FirstAsync();
                        if (DeleteCustomRequest != null)
                            DeleteCustomRequest.IsActive = false;
                        await _context.SaveChangesAsync();
                        var callbackUrl = Url.Action("Contact", "Home","", protocol: Request.Url.Scheme);

                        await UserManager.SendEmailAsync(ordermodel.MemberID, "Order confirmation", "<!DOCTYPE html><html><head><style>*{font-family:\"Raleway\", \"Helvetica Neue\", Helvetica, Arial, sans-serif;}.block{box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23);padding: 10px;}</style></head><body><div class=\"block\"><h1>Thank you!</h1><p>This is a confirmation email to notify that your order for customized product has been placed.</p><p>Please <a href=\"" + callbackUrl + "\">contact us</a> if you have any queries</p><p><small>Thanks and regards,<br>Team Cafe Arts</small></p></div></body></html>");

                    }

                    _context.orders.Add(ordermodel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Orders");
                }

                else
                {
                    var OrderInDB = await _context.orders.SingleAsync(m => m.OrderID == ordermodel.OrderID);
                    if (OrderInDB == null)
                        return HttpNotFound();

                    TryUpdateModel(OrderInDB);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Orders");
                }
            }
        }

        public async Task<ActionResult> CustomizeRequests()
        {
            var CustomizeModel = await _context.Customizing.Include(m => m.Categories).Where(m => m.IsActive).ToListAsync();
            return View(CustomizeModel);
        }

        public async Task<ActionResult> CreateCustomize(int id)
        {
            var CustomModel = await _context.Customizing.SingleAsync(m => m.CustomizeID == id);
            if (CustomModel == null)
                return HttpNotFound();

            var OrderModel = new Order()
            {
                CustomerName = CustomModel.FullName,
                CustomerEmail = CustomModel.Email,
                CustomerContact = CustomModel.ContactNumber,
                IsCustomized = true
            };
            return View("ModifyOrder", OrderModel);
        }

        public async Task<ActionResult> DeleteCustomize(int id)
        {
            var RemoveModel = await _context.Customizing.SingleAsync(m => m.CustomizeID == id);

            if (RemoveModel == null)
                return HttpNotFound();

            RemoveModel.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("CustomizeRequests");
        }

        public async Task<ActionResult> Queries()
        {
            var QueryModel = await _context.Feedbacks.ToListAsync();
            return View(QueryModel);
        }

        public ActionResult SlideImages()
        {
            return View();
        }

        public ActionResult PostSlides(HttpPostedFileBase Slide1, HttpPostedFileBase Slide2, HttpPostedFileBase Slide3)
        {
            if(Slide1 != null)
            {
                string path = Path.Combine(Server.MapPath("~/Content/Img"), "Slide1.jpg");
                Slide1.SaveAs(path);
            }

            if (Slide2 != null)
            {
                string path = Path.Combine(Server.MapPath("~/Content/Img"), "Slide2.jpg");
                Slide2.SaveAs(path);
            }

            if (Slide3 != null)
            {
                string path = Path.Combine(Server.MapPath("~/Content/Img"), "Slide3.jpg");
                Slide3.SaveAs(path);
            }

            TempData["SlideSuccess"] = "Submit success!";
            return RedirectToAction("SlideImages");
        }
    }
}