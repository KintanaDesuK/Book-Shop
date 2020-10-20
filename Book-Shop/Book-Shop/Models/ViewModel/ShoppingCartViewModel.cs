using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Book> Books { get; set; }
        public Orders Orders { get; set; }
    }
}