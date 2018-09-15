using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diplom_WebSite_Taras.DAL.Entities;
using Diplom_WebSite_Taras.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using PagedList;

namespace Diplom_WebSite_Taras.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
             HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Order
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string orderSearch, string startDate, string endDate, string orderSortOrder, int? page)
        {
            var orders = db.Orders
                .Include(u=>u.ApplicationUserOf)
                .Include(u=>u.ApplicationUserOf.UserProfile)
                .OrderBy(o => o.DateCreated).Include(o => o.OrderLines);

            if (!User.IsInRole("Administrator"))
            {
                orders = orders.Where(o => o.UserId == User.Identity.Name);
            }

            if (!String.IsNullOrEmpty(orderSearch))
            {
                orders = orders.Where(o => o.OrderId.ToString().Equals(orderSearch) ||
                 o.UserId.Contains(orderSearch) ||
                 o.DeliveryName.Contains(orderSearch) ||                 
                 o.TotalPrice.ToString().Equals(orderSearch) ||
                 o.OrderLines.Any(ol => ol.ProductName.Contains(orderSearch)));
            }

            DateTime parsedStartDate;
            if (DateTime.TryParse(startDate, out parsedStartDate))
            {
                orders = orders.Where(o => o.DateCreated >= parsedStartDate);
            }

            DateTime parsedEndDate;
            if (DateTime.TryParse(endDate, out parsedEndDate))
            {
                orders = orders.Where(o => o.DateCreated <= parsedEndDate);
            }

            ViewBag.DateSort = String.IsNullOrEmpty(orderSortOrder) ? "date" : "";
            ViewBag.UserSort = orderSortOrder == "user" ? "user_desc" : "user";
            ViewBag.PriceSort = orderSortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentOrderSearch = orderSearch;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            switch (orderSortOrder)
            {
                case "user":
                    orders = orders.OrderBy(o => o.UserId);
                    break;
                case "user_desc":
                    orders = orders.OrderByDescending(o => o.UserId);
                    break;
                case "price":
                    orders = orders.OrderBy(o => o.TotalPrice);
                    break;
                case "price_desc":
                    orders = orders.OrderByDescending(o => o.TotalPrice);
                    break;
                case "date":
                    orders = orders.OrderBy(o => o.DateCreated);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.DateCreated);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Order/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.OrderLines).Where(o => o.OrderId == id).SingleOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.UserId == User.Identity.Name || User.IsInRole("Administrator") || User.IsInRole("User"))
            {
                return View(order);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

        }

        // GET: Orders/Review
        //[Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult> Review()
        {
            Cart cart = Cart.GetCart();
            Order order = new Order();

            order.UserId = User.Identity.Name;
            ApplicationUser user = await UserManager.FindByNameAsync(order.UserId);
            order.DeliveryName = user.Email;
           order.UserId = user.Id;            
            order.OrderLines = new List<OrderLine>();
            foreach (var cartLine in cart.GetCartLines())
            {
                OrderLine line = new OrderLine
                {
                    Product = cartLine.Product,
                    ProductId = cartLine.ProductId,
                    ProductName = cartLine.Product.Name,
                    Quantity = cartLine.Quantity,
                    UnitPrice = cartLine.Product.Price
                };
                order.OrderLines.Add(line);
            }
            order.TotalPrice = cart.GetTotalCost();
            return View(order);
        }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Administrator, User")]        
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,DeliveryName")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateCreated = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();

                //add the orderlines to the database after creating the order
                Cart cart = Cart.GetCart();
                order.TotalPrice = cart.CreateOrderLines(order.OrderId);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.OrderId });                
            }
            return RedirectToAction("Review");
        }

        // GET: Admin/Order/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,DeliveryName,TotalPrice,DateCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

