using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models
{
    public class ProductsSelectedForOrder
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}