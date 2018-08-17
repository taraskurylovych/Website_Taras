using Diplom_WebSite_Taras.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.Models
{
    public class CartViewModel
    {
        public List<CartLine> CartLines { get; set; }
        [Display(Name = "Разом у кошику:")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCost { get; set; }
    }
}