using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models.ViewModel
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}
