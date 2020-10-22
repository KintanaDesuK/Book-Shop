using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Models;
using Book_Shop.Models.ViewModel;
using Book_Shop.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public BookViewModel BooksVM { get; set; }
        public BooksController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            BooksVM = new BookViewModel()
            {
                Categories = _db.Categories.ToList(),
                Book = new Models.Book(),
                Authors = _db.Authors.ToList(),
                Publishers = _db.Publishers.ToList()
            };
        }
        public async Task<IActionResult> Index()
        {
            var books = _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m=> m.Publishers);
            return View(await books.ToListAsync());
        }
        public  IActionResult Create()
        {
            return View(BooksVM);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if(!ModelState.IsValid)
            {
                return View(BooksVM);
            }
            _db.Books.Add(BooksVM.Book);
            await _db.SaveChangesAsync();

            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var bookFromDb = _db.Books.Find(BooksVM.Book.Id);

            if(files.Count!=0)
            {
                //Image will be upload
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, BooksVM.Book.Id + extension),FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                bookFromDb.Image = @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + extension;
            }
            else
            {
                //when not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultBookImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + ".png");
                bookFromDb.Image = @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id ==null)
            {
                return NotFound();

            }
            BooksVM.Book = await _db.Books.Include(m => m.Categories).Include(m => m.Authors).Include(m => m.Publishers).SingleOrDefaultAsync(m => m.Id == id);
            
            if(BooksVM.Book == null)
            {
                return NotFound();
            }
            return View(BooksVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var bookFromDb = _db.Books.Where(m => m.Id == BooksVM.Book.Id).FirstOrDefault();

                if(files.Count>0 && files[0]!=null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(bookFromDb.Image);

                    if(System.IO.File.Exists(Path.Combine(uploads, BooksVM.Book.Id +extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, BooksVM.Book.Id + extension_old));
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, BooksVM.Book.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    BooksVM.Book.Image = @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + extension_new;
                }

                if(BooksVM.Book.Image != null)
                {
                    bookFromDb.Image = BooksVM.Book.Image;
                }

                bookFromDb.Name = BooksVM.Book.Name;
                bookFromDb.Price = BooksVM.Book.Price;
                bookFromDb.Available = BooksVM.Book.Available;
                bookFromDb.CategoryId = BooksVM.Book.CategoryId;
                bookFromDb.AuthorId = BooksVM.Book.AuthorId;
                bookFromDb.PublisherId = BooksVM.Book.PublisherId;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                    
            }
            return View(BooksVM);
        }
    }
}
