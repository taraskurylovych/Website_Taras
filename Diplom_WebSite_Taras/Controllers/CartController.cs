using Diplom_WebSite_Taras.DAL.Entities;
using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom_WebSite_Taras.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            Cart cart = Cart.GetCart();
            CartViewModel viewModel = new CartViewModel
            {
                CartLines = cart.GetCartLines(),
                TotalCost = cart.GetTotalCost()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int id, int quantity)
        {
            Cart cart = Cart.GetCart();
            cart.AddToCart(id, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(CartViewModel viewModel)
        {
            Cart cart = Cart.GetCart();
            cart.UpdateCart(viewModel.CartLines);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemoveLine(int id)
        {
            Cart cart = Cart.GetCart();
            cart.RemoveLine(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult Summary()
        {
            Cart cart = Cart.GetCart();
            CartSummaryViewModel viewModel = new CartSummaryViewModel
            {
                NumberOfItems = cart.GetNumberOfItems(),
                TotalCost = cart.GetTotalCost()
            };
            return PartialView(viewModel);
        }
    }
}