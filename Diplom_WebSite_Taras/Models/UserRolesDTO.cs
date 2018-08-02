using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom_WebSite_Taras.Models
{
        public class ExpandedUserDTO
        {
            [Key]
            [Display(Name = "Ім'я користувача")]
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            //[Display(Name = "Lockout End Date Utc")]
            public DateTime? LockoutEndDateUtc { get; set; }
            public int AccessFailedCount { get; set; }
            public string PhoneNumber { get; set; }
            [Display (Name = "Ролі")]
            public IEnumerable<UserRolesDTO> Roles { get; set; }
        }

        public class UserRolesDTO
        {
            [Key]
            [Display(Name = "Назва ролі")]
            public string RoleName { get; set; }
        }

        public class UserRoleDTO
        {
            [Key]
            [Display(Name = "Ім'я користувача")]
            public string UserName { get; set; }
            [Display(Name = "Назва ролі")]
            public string RoleName { get; set; }
        }

        public class RoleDTO
        {
            [Key]
            public string Id { get; set; }
            [Display(Name = "Назва ролі")]
            public string RoleName { get; set; }
        }

        public class UserAndRolesDTO
        {
            [Key]
            [Display(Name = "Ім'я користувача")]
            public string UserName { get; set; }
            public List<UserRoleDTO> colUserRoleDTO { get; set; }
        }
}