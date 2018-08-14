using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    public partial class Order
    {
            [Key]
            public int OrderId { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public decimal Total { get; set; }
            public System.DateTime OrderDate { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
      }
}