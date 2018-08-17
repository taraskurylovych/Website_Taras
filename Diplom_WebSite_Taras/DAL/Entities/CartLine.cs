using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class CartLine
    {
        [Key]
        public int Id { get; set; }
        public string CartId { get; set; }
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Введіть назву продукта")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Product Product { get; set; }
    }
}