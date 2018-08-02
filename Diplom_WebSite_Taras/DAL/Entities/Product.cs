using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 255)]
        public string Name { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "Введіть категорію")]
        public int CategoryId { get; set; }
        [ForeignKey("Producer")]
        [Required(ErrorMessage = "Введіть виробника")]
        public int ProducerId { get; set; }
        [Required(ErrorMessage = "Введіть ціну")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Введіть кількість")]
        public double Quantity { get; set; }
        [Required, StringLength(maximumLength: 255)]
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual Category Category { get; set; }
        public virtual Producer Producer { get; set; }
    }
}