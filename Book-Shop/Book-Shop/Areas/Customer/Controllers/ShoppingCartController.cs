using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Extensions;
using Book_Shop.Models;
using Book_Shop.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Books = new List<Models.Book>()
            };
        }
        public async Task<IActionResult> Index()
        {
            int itemcount;
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count == null)
            {
                itemcount = 0;
            }
            else
            {
                itemcount = lstShoppingCart.Count;
            }
            if (itemcount > 0)
            {
                foreach (int cartItem in lstShoppingCart)
                {
                    Book book = _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).Where(m => m.Id == cartItem).FirstOrDefault();
                    ShoppingCartVM.Books.Add(book);
                }
            }
            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            DateTime d1 = DateTime.Now;
            ShoppingCartVM.Orders.ShopDate = d1.Date;
            
            Orders orders = ShoppingCartVM.Orders;
            _db.Orders.Add(orders);
            _db.SaveChanges();

            int orderId = orders.Id;

            foreach (int bookId in lstCartItems)
            {
                ProductsSelectedForOrder orderItems = new ProductsSelectedForOrder()
                {
                    OrderId = orderId,
                    BookId = bookId
                };
                _db.ProductsSelectedForOrders.Add(orderItems);

            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction("OrderConfirmation", "ShoppingCart", new { Id = orderId });

        }
        public IActionResult Remove(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems.Count > 0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult OrderConfirmation(int id)
        {
            ShoppingCartVM.Orders = _db.Orders.Where(a => a.Id == id).FirstOrDefault();
            List<ProductsSelectedForOrder> objProdList = _db.ProductsSelectedForOrders.Where(p => p.OrderId == id).ToList();

            foreach (ProductsSelectedForOrder prodAptObj in objProdList)
            {
                ShoppingCartVM.Books.Add(_db.Books.Include(p => p.Categories).Include(p => p.Authors).Include(p => p.Publishers).Where(p => p.Id == prodAptObj.BookId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }
    }
}
