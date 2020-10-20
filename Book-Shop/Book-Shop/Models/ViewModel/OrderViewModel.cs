using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Models.ViewModel
{
    public class OrderViewModel
    {
        public List<Orders> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}