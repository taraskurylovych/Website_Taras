using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 255)]
        [Display(Name = "Назва виробника")]
        public string Name { get; set; }
        [Display(Name = "Опубліковано")]
        public bool Published { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}