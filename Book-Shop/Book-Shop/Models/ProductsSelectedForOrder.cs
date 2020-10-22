using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models
{
    public class ProductsSelectedForOrder
    {
        public int Id { get; set; }

        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }

        [Display(Name = "Book")]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}