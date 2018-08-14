using Diplom_WebSite_Taras.Areas.Admin.Models;
using Diplom_WebSite_Taras.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom_WebSite_Taras.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index(int? page)
        {
            List<ProductItemViewModel> model =
                 _context.Products
                 //.Include(c => c.Category)
                 //.Include(pr => pr.Producer)
                 .Select(p => new ProductItemViewModel
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Price = p.Price,
                     Quantity = p.Quantity,
                     Description = p.Description,
                     CategoryName = p.Category.Name,
                     ProducerName = p.Producer.Name,
                     Image = p.Image

                 }).ToList();
            
            return View(model.ToList().ToPagedList(page ?? 1, 5));

        }

        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            ProductItemViewModel model = new ProductItemViewModel();
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Quantity = product.Quantity;
                model.Description = product.Description;
                model.CategoryName = product.Category.Name;
                model.ProducerName = product.Producer.Name;
                model.Image = product.Image;
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Про нас і нашу ідею.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Наші контакти:";

            return View();
        }

        public ActionResult FreeDelivery500()
        {
            ViewBag.Message = "Безкоштовна доставка по Україні від 500грн.";

            return View();
        }

        public ActionResult Warranty()
        {
            ViewBag.Message = "2 роки повноцінної гарантії на усі матраси";

            return View();
        }

        public ActionResult Oplata()
        {
            ViewBag.Message = "Оплати товар частинами";

            return View();
        }
    }
}

//path content