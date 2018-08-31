using Diplom_WebSite_Taras.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.DAL.Entities
{
    [Table("tblUserProfiles")]
    public class UserProfile
    {
        [Key, ForeignKey("ApplicationUserOf")]
        public string Id { get; set; }
        [Required, StringLength(100)]
        [Display (Name="Ім'я")]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Required, StringLength(1000)]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        public virtual ApplicationUser ApplicationUserOf { get; set; }
    }
}