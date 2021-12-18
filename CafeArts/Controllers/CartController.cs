using CafeArts.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CafeArts.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public CartController()
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

        // GET: Cart
        [HttpGet]
        public async Task<ActionResult> MyCart()
        {
            var AllCarts = await _context.Carts.Include(m => m.Product).ToListAsync();
            var CartInDB = AllCarts.FindAll(m => m.MemberID == User.Identity.GetUserId() && m.IsActive);
            return View(CartInDB);
        }

        
        [PreventFromUrl]
        public async Task<ActionResult> AddToCart(int id, int? quantity)
        {
            var CurrentUser = User.Identity.GetUserId();

            var CartModel = new Cart { MemberID = User.Identity.GetUserId(), ProductID = id };
            if (quantity != null)
                CartModel.Quantity = quantity;

            CartModel.IsActive = true;

            var SearchModel = _context.Carts.Where(m => m.MemberID == CurrentUser && m.ProductID == id && m.IsActive);

            if (await SearchModel.CountAsync()!=0)
            {
                foreach(var cartitem in SearchModel)
                    _context.Carts.Remove(cartitem);

                TempData["Message"] = "Cart updated";
                
            }

            else
            {
                
                TempData["Message"] = "Item added succesfully";
                
            }
            _context.Carts.Add(CartModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyCart", "Cart");

        }

        [PreventFromUrl]
        public async Task<ActionResult> DeleteFromCart(int id)
        {
            var DeleteModel = await _context.Carts.SingleAsync(m => m.CartID == id);
            if (DeleteModel == null)
                return HttpNotFound();

            _context.Carts.Remove(DeleteModel);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Item removed succesfully";

            return RedirectToAction("MyCart", "Cart");
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult CartCount()
        {
            var CurrentUser = User.Identity.GetUserId();
            var CartItemCount = _context.Carts.Where(m => m.MemberID == CurrentUser && m.IsActive).Count();
            ViewBag.CartItemCount = CartItemCount.ToString();

            return PartialView("_CartIcon");

        }

        [PreventFromUrl]
        public async Task<ActionResult> ShippingInformation()
        {
            var currentUser = User.Identity.GetUserId();

            ViewBag.CartDetails = _context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive);

            ViewBag.CartItemSubTotal = CalculateTotal(ViewBag.CartDetails);

            ViewBag.CountItems = _context.Carts.Where(m => m.MemberID == currentUser && m.IsActive).Count();

            ViewBag.UserDetails = await _context.Users.SingleAsync(m => m.Id == currentUser);

            if (ViewBag.UserDetails == null)
                return HttpNotFound();

            ViewBag.StateList = await _context.State.ToListAsync();

            var ShippingModel = await _context.ShippingDets.Include(m => m.ApplicationUsers).SingleOrDefaultAsync(m => m.MemberID == currentUser);

            return View(ShippingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveShippingAddress(ShippingDetails shippingmodel)
        {
            var currentUser = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {               
                ViewBag.CartDetails = _context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive);

                ViewBag.CartItemSubTotal = CalculateTotal(ViewBag.CartDetails);

                ViewBag.CountItems = _context.Carts.Where(m => m.MemberID == currentUser && m.IsActive).Count();
                ViewBag.UserDetails = await _context.Users.SingleAsync(m => m.Id == currentUser);
                if (ViewBag.UserDetails == null)
                    return HttpNotFound();
                ViewBag.StateList = await _context.State.ToListAsync();

                return View("ShippingInformation", shippingmodel);
            }

            else
            {
                if(shippingmodel.ShippingDetailsID == null)
                {
                    shippingmodel.MemberID = currentUser;
                    shippingmodel.Amount = CalculateTotal(_context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive));
                    _context.ShippingDets.Add(shippingmodel);
                }

                else
                {
                    var ShippingInDB = await _context.ShippingDets.SingleAsync(m => m.ShippingDetailsID == shippingmodel.ShippingDetailsID);
                    if (ShippingInDB == null)
                        return HttpNotFound();
                    ShippingInDB.MemberID = currentUser;
                    ShippingInDB.Amount = CalculateTotal(_context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive));
                    ShippingInDB.Address = shippingmodel.Address;
                    ShippingInDB.City = shippingmodel.City;
                    ShippingInDB.StateID = shippingmodel.StateID;
                    ShippingInDB.ZipCode = shippingmodel.ZipCode;
                    ShippingInDB.FName = shippingmodel.FName;
                    ShippingInDB.LName = shippingmodel.LName;
                    ShippingInDB.PhoneNumber = shippingmodel.PhoneNumber;
                    ShippingInDB.ShippingType = shippingmodel.ShippingType;

                }
                
                await _context.SaveChangesAsync();



                return RedirectToAction("PaymentMethod");
            }  
        }

        [PreventFromUrl]
        public ActionResult PaymentMethod()
        {
            return View();
        }

        [PreventFromUrl]
        public async Task<ActionResult> OtherPayment()
        {
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(WebConfigurationManager.AppSettings["RazorPayId"], WebConfigurationManager.AppSettings["RazorPaySecret"]);
            var curUser = User.Identity.GetUserId();
            var ShippingDetailsModel = await _context.ShippingDets.SingleAsync(m => m.MemberID == curUser);
            if (ShippingDetailsModel == null)
                return HttpNotFound();
            var UserModel = await _context.Users.SingleAsync(m => m.Id == curUser);

            if (UserModel == null)
                return HttpNotFound();

            Dictionary<string, object> options = new Dictionary<string, object>();
            if (ShippingDetailsModel.Amount == 0)
                return HttpNotFound();
            options.Add("amount", ShippingDetailsModel.Amount * 100);
            options.Add("receipt", new Random().Next(10000000, 100000000).ToString());
            options.Add("currency", "INR");
            options.Add("payment_capture", "0");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            var OrderModel = new Order()
            {
                TransactionID = orderId,
                RazorPayKey = WebConfigurationManager.AppSettings["RazorPayId"],
                TotalAmount = CalculateTotal(_context.Carts.Include(m => m.Product).Where(m => m.MemberID == curUser)),
                CustomerName = UserModel.FirstName + " " + UserModel.LastName,
                CustomerEmail = UserModel.Email,
                CustomerContact = ShippingDetailsModel.PhoneNumber,
                CustomerAddress = ShippingDetailsModel.Address,
            };

            return View(OrderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmation()
        {

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Params["rzp_paymentid"];

            // This is orderId
            string transactionId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(WebConfigurationManager.AppSettings["RazorPayId"], WebConfigurationManager.AppSettings["RazorPaySecret"]);

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            var AmountInFLoat = float.Parse(amt);



            TempData["TransactionID"] = transactionId;
            TempData["RPUniquePaymentID"] = paymentId;
            TempData["TotalAmount"] = AmountInFLoat / 100;
            TempData["RouteFromOnline"] = true;
                

            

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {

                return RedirectToAction("OrderSuccess", "Cart");
            }
            else
            {
                return RedirectToAction("PaymentFailed", "Cart");
            }
        }

        [PreventFromUrl]
        public async Task<ActionResult> OrderSuccess()
        {
            var CurrentUser = User.Identity.GetUserId();
            var ShippingDetailsModel = await _context.ShippingDets.SingleAsync(m => m.MemberID == CurrentUser);
            if (ShippingDetailsModel == null)
                return HttpNotFound();
            var UserModel = await _context.Users.SingleAsync(m => m.Id == CurrentUser);
            if (UserModel == null)
                return HttpNotFound();

            var order = new Order()
            {

                MemberID = CurrentUser,
                CustomerName = UserModel.FirstName + " " + UserModel.LastName,
                CustomerEmail = UserModel.Email,
                CustomerContact = ShippingDetailsModel.PhoneNumber,
                CustomerAddress = ShippingDetailsModel.Address,
                CreatedDate = DateTime.Now,
                RazorPayKey = "Not available",
                OrderStatus = "Ordered",
                WayBill = "NA"

            };

            if (TempData.ContainsKey("RouteFromOnline"))
            {
                order.TotalAmount = (float)TempData["TotalAmount"];
                order.OrderType = "Online";
                order.TransactionID = TempData["TransactionID"].ToString();
                order.RPUniquePaymentID = TempData["RPUniquePaymentID"].ToString();
            }

            else
            {
                order.TotalAmount = (float)ShippingDetailsModel.Amount;
                order.OrderType = "Cash on delivery";
                order.TransactionID = "Not Available";
                order.RPUniquePaymentID = "Not Available";
            }
            

            _context.orders.Add(order);

            await _context.SaveChangesAsync();

            var CartModel = _context.Carts.Where(m => m.MemberID == order.MemberID && m.IsActive);

            foreach (var modifyCart in CartModel)
            {
                modifyCart.IsActive = false;
                modifyCart.OrderID = order.OrderID;
            }

            await _context.SaveChangesAsync();

            var callbackUrl = Url.Action("OrderPlaced", "Cart", new { id = order.OrderID }, protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(CurrentUser, "Order confirmation", "<!DOCTYPE html><html><head><style>*{font-family:\"Raleway\", \"Helvetica Neue\", Helvetica, Arial, sans-serif;}.block{box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23);padding: 10px;}</style></head><body><div class=\"block\"><h1>Thank you!</h1><p>This is a confirmation email to notify that you have placed an order with us.</p><p>To view your order summary, please click <a href=\"" + callbackUrl + "\">here</a></p><p><small>Thanks and regards,<br>Team Cafe Arts</small></p></div></body></html>");

            return RedirectToAction("OrderPlaced","Cart", new { id = order.OrderID});

        }
       
        [PreventFromUrl]
        public async Task<ActionResult> OrderPlaced(int id)
        {
            var CurrentUser = User.Identity.GetUserId();
            var CartDetailsModel = _context.Carts.Include(m => m.Product).Where(m => m.OrderID == id);
            ViewBag.OrderDetails = await _context.orders.SingleAsync(m => m.OrderID == id && m.MemberID == CurrentUser);
            if (ViewBag.OrderDetails == null)
                return HttpNotFound();

            return View(CartDetailsModel);
        }

        [PreventFromUrl]
        public ActionResult PaymentFailed()
        {
            return View();
        }

        [NonAction]
        public float CalculateTotal(IEnumerable<Cart> cartmodel)
        {
            float? subtotal = 0;
            foreach(var total in cartmodel)
            {
                subtotal += total.Product.ProductPrice * total.Quantity;
            }
            return subtotal??0;
        }

    }
}