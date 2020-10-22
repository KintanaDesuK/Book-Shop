using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Book_Shop.Models;
using Book_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Book_Shop.Extensions;
using Book_Shop.Models.ViewModel;
using System.Text;

namespace Book_Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShopViewModel ShopVM { get; set; }

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
            ShopVM = new ShopViewModel()
            {
                Books = new List<Models.Book>(),
                Categories = new List<Models.Category>(),
                Authors = new List<Models.Author>(),
                Publishers = new List<Models.Publisher>()
            };
        }
        public async Task<IActionResult> Index(string searchName = null)
        {

            StringBuilder param = new StringBuilder();
            param.Append("&searchName=");

            if (searchName != null)
            {
                param.Append(searchName);
            }


            int count = 0;
            var bookList = await _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).ToListAsync();
            var categoryList = await _db.Categories.ToListAsync();
            var authorList = await _db.Authors.ToListAsync();
            var publisherList = await _db.Publishers.ToListAsync();


            if (searchName != null)
            {
                bookList = await _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).Where(a => a.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();

                for (int i = 0; i < bookList.Count; i++)
                {
                    ShopVM.Books.Add(bookList[i]);
                    count++;
                }
            }
            else
            {

                for (int i = 0; i < bookList.Count; i++)
                {
                    ShopVM.Books.Add(bookList[i]);
                    count++;
                }

            }
            for (int i = 0; i < categoryList.Count; i++)
            {
                ShopVM.Categories.Add(categoryList[i]);
            }
            for (int i = 0; i < authorList.Count; i++)
            {
                ShopVM.Authors.Add(authorList[i]);
            }
            for (int i = 0; i < publisherList.Count; i++)
            {
                ShopVM.Publishers.Add(publisherList[i]);
            }
            HttpContext.Session.Set("ssAmount", count);

            return View(ShopVM);
            //return RedirectToAction("Index", "ssAmount");
        }

        public async Task<IActionResult> SearchIndex(String searchName)
        {

            var list = from m in _db.Books
                       select m;
            if (searchName != null)
            {
                list = list.Where(s => s.Name.Contains(searchName));
            }
            return View(await list.ToListAsync());

        }

        public async Task<IActionResult> SearchCategory(int id)
        {
            int count = HttpContext.Session.Get<int>("ssAmount");
            count = 0;
            var bookList = _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).Where(m => m.CategoryId == id).ToList();
            var categoryList = _db.Categories.ToList();
            var authorList = _db.Authors.ToList();
            var publisherList = _db.Publishers.ToList();
            for (int i = 0; i < bookList.Count; i++)
            {
                ShopVM.Books.Add(bookList[i]);
                count++;
            }
            for (int i = 0; i < categoryList.Count; i++)
            {
                ShopVM.Categories.Add(categoryList[i]);
            }
            for (int i = 0; i < authorList.Count; i++)
            {
                ShopVM.Authors.Add(authorList[i]);
            }
            for (int i = 0; i < publisherList.Count; i++)
            {
                ShopVM.Publishers.Add(publisherList[i]);
            }
            HttpContext.Session.Set("ssAmount", count);
            return View(ShopVM);

        }
        public async Task<IActionResult> SearchAuthor(int id)
        {
            int count = HttpContext.Session.Get<int>("ssAmount");
            count = 0;
            var bookList = _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).Where(m => m.AuthorId == id).ToList();
            var categoryList = _db.Categories.ToList();
            var authorList = _db.Authors.ToList();
            var publisherList = _db.Publishers.ToList();
            for (int i = 0; i < bookList.Count; i++)
            {
                ShopVM.Books.Add(bookList[i]);
                count++;
            }
            for (int i = 0; i < categoryList.Count; i++)
            {
                ShopVM.Categories.Add(categoryList[i]);
            }
            for (int i = 0; i < authorList.Count; i++)
            {
                ShopVM.Authors.Add(authorList[i]);
            }
            for (int i = 0; i < publisherList.Count; i++)
            {
                ShopVM.Publishers.Add(publisherList[i]);
            }
            HttpContext.Session.Set("ssAmount", count);
            return View(ShopVM);

        }
        public async Task<IActionResult> SearchPublisher(int id)
        {
            int count = HttpContext.Session.Get<int>("ssAmount");
            count = 0;
            var bookList = _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).Where(m => m.PublisherId == id).ToList();
            var categoryList = _db.Categories.ToList();
            var authorList = _db.Authors.ToList();
            var publisherList = _db.Publishers.ToList();
            for (int i = 0; i < bookList.Count; i++)
            {
                ShopVM.Books.Add(bookList[i]);
                count++;
            }
            for (int i = 0; i < categoryList.Count; i++)
            {
                ShopVM.Categories.Add(categoryList[i]);
            }
            for (int i = 0; i < authorList.Count; i++)
            {
                ShopVM.Authors.Add(authorList[i]);
            }
            for (int i = 0; i < publisherList.Count; i++)
            {
                ShopVM.Publishers.Add(publisherList[i]);
            }
            HttpContext.Session.Set("ssAmount", count);
            return View(ShopVM);

        }
    }
}
