using Diplom_WebSite_Taras.Areas.Admin.Models;
using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Entity;
using System.Transactions;
using Diplom_WebSite_Taras.DAL.Entities;
using PagedList;

namespace Diplom_WebSite_Taras.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin/Product
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string currentFilter, string search, int? page)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            List<ProductItemViewModel> model =
                query                
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
          return View(model.ToList().ToPagedList(page ?? 1, 7));           
        }
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            
            model.SelectListCategories = _context.Categories
                .Select(r => new SelectItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();

            model.SelectListProducers = _context.Producers
                .Select(r => new SelectItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = Guid.NewGuid().ToString() + ".jpg";
                    Product product = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Description = model.Description,
                        CategoryId = model.CategoryId,
                        ProducerId = model.ProducerId,
                        Image=filename
                    };
                    _context.Products.Add(product);

                    _context.SaveChanges();

                    string base64image = model.ImageBase64.Split(',')[1];
                    Bitmap imgCropped = base64image.FromBase64StringToBitmap();
                    string path = Server.MapPath("~/Upload/UserImages/");
                    
                    imgCropped.Save(path + filename, ImageFormat.Jpeg);

                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Щось пішло не так!");

                }


            }
            model.SelectListCategories = _context.Categories
             .Select(r => new SelectItemViewModel
             {
                 Id = r.Id,
                 Name = r.Name
             }).ToList();

            model.SelectListProducers = _context.Producers
                .Select(r => new SelectItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {

            ProductEditViewModel model = new ProductEditViewModel();

            Product product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product != null)
            {
                model.CategoryId = product.CategoryId;
                model.ProducerId = product.ProducerId;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Description = product.Description;
                model.Quantity = product.Quantity;
                model.Id = product.Id;
                


            }
            model.SelectListCategories = _context.Categories
        .Select(r => new SelectItemViewModel
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();

            model.SelectListProducers = _context.Producers
                .Select(r => new SelectItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();



            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = _context.Products.SingleOrDefault(p => p.Id == model.Id);

                if (product != null)
                {
                    product.ProducerId = model.ProducerId;
                    product.CategoryId = model.CategoryId;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.Quantity = model.Quantity;
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            model.SelectListCategories = _context.Categories
      .Select(r => new SelectItemViewModel
      {
          Id = r.Id,
          Name = r.Name
      }).ToList();

            model.SelectListProducers = _context.Producers
                .Select(r => new SelectItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            ProductItemViewModel model = new ProductItemViewModel();
            var product = _context.Products.SingleOrDefault(pr => pr.Id == id);
            if (product != null)
            {
                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Quantity = product.Quantity;
                model.Description = product.Description;
                model.CategoryName = product.Category.Name;
                model.ProducerName = product.Producer.Name;   
                            
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductItemViewModel model)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == model.Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
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
            }
            return View(model);
        }
    }
}