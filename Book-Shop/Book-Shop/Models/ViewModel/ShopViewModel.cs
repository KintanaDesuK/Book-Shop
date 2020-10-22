using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models.ViewModel
{
    public class ShopViewModel
    {
        public List<Book> Books { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
    }
}
