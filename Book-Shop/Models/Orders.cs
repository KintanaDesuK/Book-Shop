using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models
{
    public class Orders
    {
        public int Id { get; set; }

        [Display(Name = "Admin User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser AdminUser { get; set; }

        public DateTime ShopDate { get; set; }

        [NotMapped]
        public DateTime ShopTime { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }

        public string Address { get; set; }
        public bool isConfirmed { get; set; }
    }
}