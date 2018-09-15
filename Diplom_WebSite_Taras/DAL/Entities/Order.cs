using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class Order
    {
        [Key]
        [Display(Name = "Id замовлення")]
        public int OrderId { get; set; }
        [Display(Name = "Id Користувача"), ForeignKey("ApplicationUserOf")]
        public string UserId { get; set; }
        [Display(Name = "Отримувач")]
        public string DeliveryName { get; set; }        
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Загальна сума")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Час замовлення")]
        public DateTime DateCreated { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public virtual ApplicationUser ApplicationUserOf { get; set; }
    }
}