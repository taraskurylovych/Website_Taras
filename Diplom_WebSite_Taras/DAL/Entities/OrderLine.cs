using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        [Display(Name = "Кількість")]
        public int Quantity { get; set; }
        [Display(Name = "Назва продукту")]
        public string ProductName { get; set; }
        [Display(Name = "Ціна за одиницю")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal UnitPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}