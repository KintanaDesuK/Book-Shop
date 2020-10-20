using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models
{
    public class ApplicationUser : IdentityUser

    {
        [Display(Name = "UserName")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsSuperAdmin { get; set; }
    }
}