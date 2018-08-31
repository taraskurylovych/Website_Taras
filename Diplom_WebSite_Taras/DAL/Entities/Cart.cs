using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class Cart
    {
        private string CartId { get; set; }
        private const string CartSessionKey = "CartId";
        private ApplicationDbContext db = new ApplicationDbContext();

        private string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] =
                 HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public static Cart GetCart()
        {
            Cart cart = new Cart();
            cart.CartId = cart.GetCartId();
            return cart;
        }

        public void AddToCart(int productId, int quantity)
        {
            var cartLine = db.CartLines.FirstOrDefault(b => b.CartId == CartId && b.ProductId
             == productId);

            if (cartLine == null)
            {
                cartLine = new CartLine
                {
                    ProductId = productId,
                    CartId = CartId,
                    Quantity = quantity,
                    DateCreated = DateTime.Now
                };
                db.CartLines.Add(cartLine);
            }
            else
            {
                cartLine.Quantity += quantity;
            }
            db.SaveChanges();
        }

        public void RemoveLine(int productId)
        {
            var cartLine = db.CartLines.FirstOrDefault(b => b.CartId == CartId && b.ProductId
             == productId);
            if (cartLine != null)
            {
                db.CartLines.Remove(cartLine);
            }
            db.SaveChanges();
        }

        public void UpdateCart(List<CartLine> lines)
        {
            foreach (var line in lines)
            {
                var cartLine = db.CartLines.FirstOrDefault(b => b.CartId == CartId &&
                 b.ProductId == line.ProductId);
                if (cartLine != null)
                {
                    if (line.Quantity == 0)
                    {
                        RemoveLine(line.ProductId);
                    }
                    else
                    {
                        cartLine.Quantity = line.Quantity;
                    }
                }
            }
            db.SaveChanges();
        }

        public void EmptyCart()
        {
            var cartLines = db.CartLines.Where(b => b.CartId == CartId);
            foreach (var cartLine in cartLines)
            {
                db.CartLines.Remove(cartLine);
            }
            db.SaveChanges();
        }

        public List<CartLine> GetCartLines()
        {
            return db.CartLines.Where(b => b.CartId == CartId).ToList();
        }

        public decimal GetTotalCost()
        {
            decimal cartTotal = decimal.Zero;

            if (GetCartLines().Count > 0)
            {
                cartTotal = db.CartLines.Where(b => b.CartId == CartId).Sum(b => b.Product.Price
                 * b.Quantity);
            }

            return cartTotal;
        }

        public int GetNumberOfItems()
        {
            int numberOfItems = 0;
            if (GetCartLines().Count > 0)
            {
                numberOfItems = db.CartLines.Where(b => b.CartId == CartId).Sum(b => b.Quantity);
            }

            return numberOfItems;
        }

        public void MigrateCart(string userName)
        {
            //find the current cart and store it in memory using ToList()
            var cart = db.CartLines.Where(b => b.CartId == CartId).ToList();

            //find if the user already has a cart or not and store it in memory using ToList()
            var usersCart = db.CartLines.Where(b => b.CartId == userName).ToList();

            //if the user has a cart then add the current items to it
            if (usersCart != null)
            {
                //set the cartID to the username
                string prevId = CartId;
                CartId = userName;
                //add the lines in anonymous cart to the user's cart
                foreach (var line in cart)
                {
                    AddToCart(line.ProductId, line.Quantity);
                }
                //delete the lines in the anonymous cart from the database
                CartId = prevId;
                EmptyCart();
            }
            else
            {
                //if the user does not have a cart then just migrate this one
                foreach (var cartLine in cart)
                {
                    cartLine.CartId = userName;
                }

                db.SaveChanges();
            }
            HttpContext.Current.Session[CartSessionKey] = userName;
        }

        public decimal CreateOrderLines(int orderId)
        {
            decimal orderTotal = 0;

            var cartLines = GetCartLines();

            foreach (var item in cartLines)
            {
                OrderLine orderLine = new OrderLine
                {
                    Product = item.Product,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    OrderId = orderId
                };

                orderTotal += (item.Quantity * item.Product.Price);
                db.OrderLines.Add(orderLine);
            }

            db.SaveChanges();
            EmptyCart();
            return orderTotal;
        }

    }
}
