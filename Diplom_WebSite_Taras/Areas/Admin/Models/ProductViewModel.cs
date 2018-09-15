using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Diplom_WebSite_Taras.Areas.Admin.Models
{
    public class ProductItemViewModel
    {

        public int Id { get; set; }
        [Required, Display (Name = "Назва продукту")]
        public string Name { get; set; }
        [Required, Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Кількість")]
        public double Quantity { get; set; }
        [Display(Name = "Опис продукту")]
        public string Description { get; set; }
        [Required, Display(Name = "Категорія")]
        public string CategoryName { get; set; }
        [Required, Display(Name = "Виробник")]
        public string ProducerName { get; set; }
        [Required, Display(Name = "Зображення")]
        public string Image { get; set; }

    }

    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва продукту")]
        public string Name { get; set; }
        [Required, Display(Name = "Категорія")]
        public int CategoryId { get; set; }        
        public List<SelectItemViewModel> SelectListCategories { get; set; }

        [Required, Display(Name = "Виробник")]
        
        public int ProducerId { get; set; }
        public List<SelectItemViewModel> SelectListProducers { get; set; }

        [Required, Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Кількість")]
        public double Quantity { get; set; }
        [Display(Name = "Опис продукту")]
        public string Description { get; set; }
      
    }

    public class ProductCreateViewModel
    {

        [Required, Display(Name = "Назва продукту")]
        public string Name { get; set; }

        [Required, Display(Name = "Категорія")]
        public int CategoryId { get; set; }
        public List<SelectItemViewModel> SelectListCategories { get; set; }
        [Required, Display(Name = "Виробник")]
        public int ProducerId { get; set; }
        public List<SelectItemViewModel> SelectListProducers { get; set; }
        [Required(ErrorMessage = "Введіть ціну"), Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Введіть кількість"), Display(Name = "Кількість")]
        public double Quantity { get; set; }
        [Required, Display(Name = "Опис продукту")]
        public string Description { get; set; }
        [Display(Name = "Обрати фото")]
        public string ImageBase64 { get; set; }
    }
}