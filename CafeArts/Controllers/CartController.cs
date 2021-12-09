using CafeArts.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CafeArts.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        

        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Cart
        [HttpGet]
        public ActionResult MyCart()
        {
            var AllCarts = _context.Carts.Include(m => m.Product).ToList();
            var CartInDB = AllCarts.FindAll(m => m.MemberID == User.Identity.GetUserId() && m.IsActive);
            return View(CartInDB);
        }

        
        [PreventFromUrl]
        public ActionResult AddToCart(int id, int? quantity)
        {
            var CurrentUser = User.Identity.GetUserId();

            var CartModel = new Cart { MemberID = User.Identity.GetUserId(), ProductID = id };
            if (quantity != null)
                CartModel.Quantity = quantity;

            CartModel.IsActive = true;

            var SearchModel = _context.Carts.Where(m => m.MemberID == CurrentUser && m.ProductID == id && m.IsActive);

            if (SearchModel.Count()!=0)
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
            _context.SaveChanges();
            return RedirectToAction("MyCart", "Cart");

        }

        [PreventFromUrl]
        public ActionResult DeleteFromCart(int id)
        {
            var DeleteModel = _context.Carts.Single(m => m.CartID == id);
            if (DeleteModel == null)
                return HttpNotFound();

            _context.Carts.Remove(DeleteModel);
            _context.SaveChanges();
            TempData["Message"] = "Item removed succesfully";

            return RedirectToAction("MyCart", "Cart");
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult CartCount()
        {
            var CurrentUser = User.Identity.GetUserId();
            ViewBag.CartItemCount = _context.Carts.Where(m => m.MemberID == CurrentUser && m.IsActive).Count().ToString();

            return PartialView("_CartIcon");

        }

        [PreventFromUrl]
        public ActionResult ShippingInformation()
        {
            var currentUser = User.Identity.GetUserId();

            ViewBag.CartDetails = _context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive);

            ViewBag.CartItemSubTotal = CalculateTotal(ViewBag.CartDetails);

            ViewBag.CountItems = _context.Carts.Where(m => m.MemberID == currentUser && m.IsActive).Count();
            ViewBag.UserDetails = _context.Users.Single(m => m.Id == currentUser);
            ViewBag.StateList = _context.State.ToList();

            var ShippingModel = _context.ShippingDets.Include(m => m.ApplicationUsers).SingleOrDefault(m => m.MemberID == currentUser);

            return View(ShippingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShippingAddress(ShippingDetails shippingmodel)
        {
            var currentUser = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {               
                ViewBag.CartDetails = _context.Carts.Include(m => m.Product).Where(m => m.MemberID == currentUser && m.IsActive);

                ViewBag.CartItemSubTotal = CalculateTotal(ViewBag.CartDetails);

                ViewBag.CountItems = _context.Carts.Where(m => m.MemberID == currentUser && m.IsActive).Count();
                ViewBag.UserDetails = _context.Users.Single(m => m.Id == currentUser);
                ViewBag.StateList = _context.State.ToList();

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
                    var ShippingInDB = _context.ShippingDets.Single(m => m.ShippingDetailsID == shippingmodel.ShippingDetailsID);
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
                
                _context.SaveChanges();



                return RedirectToAction("PaymentMethod");
            }  
        }

        [PreventFromUrl]
        public ActionResult PaymentMethod()
        {
            return View();
        }

        [PreventFromUrl]
        public ActionResult OtherPayment()
        {
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(WebConfigurationManager.AppSettings["RazorPayId"], WebConfigurationManager.AppSettings["RazorPaySecret"]);
            var curUser = User.Identity.GetUserId();
            var ShippingDetailsModel = _context.ShippingDets.Single(m => m.MemberID == curUser);
            var UserModel = _context.Users.Single(m => m.Id == curUser);

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
        public ActionResult OrderSuccess()
        {
            var CurrentUser = User.Identity.GetUserId();
            var ShippingDetailsModel = _context.ShippingDets.Single(m => m.MemberID == CurrentUser);
            var UserModel = _context.Users.Single(m => m.Id == CurrentUser);

            var order = new Order()
            {

                MemberID = CurrentUser,
                CustomerName = UserModel.FirstName + " " + UserModel.LastName,
                CustomerEmail = UserModel.Email,
                CustomerContact = ShippingDetailsModel.PhoneNumber,
                CustomerAddress = ShippingDetailsModel.Address,
                CreatedDate = DateTime.Now,
                RazorPayKey = "Not available",
                OrderStatus = "Ordered"

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

            _context.SaveChanges();

            var CartModel = _context.Carts.Where(m => m.MemberID == order.MemberID && m.IsActive);

            foreach (var modifyCart in CartModel)
            {
                modifyCart.IsActive = false;
                modifyCart.OrderID = order.OrderID;
            }

            _context.SaveChanges();

            return RedirectToAction("OrderPlaced","Cart", new { id = order.OrderID});

        }
       
        [PreventFromUrl]
        public ActionResult OrderPlaced(int id)
        {
            var CurrentUser = User.Identity.GetUserId();
            var CartDetailsModel = _context.Carts.Include(m => m.Product).Where(m => m.OrderID == id);
            ViewBag.OrderDetails = _context.orders.Single(m => m.OrderID == id && m.MemberID == CurrentUser);
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

        //[NonAction]
        //public string GetDeliveryStatus (string CurrentStatus)
        //{
        //    if (CurrentStatus == )
        //    return CurrentStatus;
        //}
    }
}