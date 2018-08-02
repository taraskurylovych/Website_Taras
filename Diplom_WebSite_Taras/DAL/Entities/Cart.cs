using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string CartId { get; set; }
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Введіть назву товару")]
        public int ProductId { get; set; }
        public double Count { get; set; }

        //[Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }
    }
}