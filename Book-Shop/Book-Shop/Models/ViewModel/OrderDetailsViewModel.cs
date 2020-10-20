using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models.ViewModel
{
    public class OrderDetailsViewModel
    {
        public Orders Orders { get; set; }
        public List<ApplicationUser> SalesPerson { get; set; }
        public List<Book> Books { get; set; }
    }
}