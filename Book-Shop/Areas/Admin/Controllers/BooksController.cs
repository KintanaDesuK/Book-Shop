using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Models.ViewModel;
using Book_Shop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Book_Shop.Models;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        [Obsolete]
        private readonly IWebHostEnvironment _hostingEnvironment;
        [BindProperty]
        public BooksViewModel BooksVM { get; set; }

        [Obsolete]
        public BooksController(ApplicationDbContext db , IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            BooksVM = new BooksViewModel()
            {
                Categories = _db.Categories.ToList(),
                Publishers = _db.Publishers.ToList(),
                Book = new Models.Book()
            };
        }
        public async Task<IActionResult> Index()
        {
            var book = _db.Books.Include(m=>m.Categories).Include(m=>m.Publisher);
            return View(await book.ToListAsync());
        }

        // Get Book Create Action Method
        public IActionResult Create()
        {
            return View(BooksVM);
        }
        // Post Book Create Action Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(BooksVM);
            }
            _db.Books.Add(BooksVM.Book);
            await _db.SaveChangesAsync();

            //image being saved
            

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var booksFromDb = _db.Books.Find(BooksVM.Book.Id);

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream = new FileStream(Path.Combine(uploads, BooksVM.Book.Id + extension),FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                booksFromDb.Image = @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + extension;
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultBookImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder +@"\" + BooksVM.Book.Id+".png");
                booksFromDb.Image = @"\" +SD.ImageFolder + @"\" + BooksVM.Book.Id + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //Get : Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            BooksVM.Book = await _db.Books.Include(m => m.Categories).Include(m => m.Publisher).SingleOrDefaultAsync(m => m.Id == id);

            if(BooksVM.Book == null)
            {
                return NotFound();
            }

            return View(BooksVM);
        }

        // Post : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult>Edit(int id)
        {
            if(ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var bookFromDb = _db.Books.Where(m => m.Id == BooksVM.Book.Id).FirstOrDefault();
         
                if(files.Count>0 && files[0] != null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(bookFromDb.Image);
                    if (System.IO.File.Exists(Path.Combine(uploads, BooksVM.Book.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, BooksVM.Book.Id + extension_old));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, BooksVM.Book.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    BooksVM.Book.Image = @"\" + SD.ImageFolder + @"\" + BooksVM.Book.Id + extension_new;
                }
                if (BooksVM.Book.Image != null)
                {
                    bookFromDb.Image = BooksVM.Book.Image;
                }
                bookFromDb.Name = BooksVM.Book.Name;
                bookFromDb.Price = BooksVM.Book.Price;
                bookFromDb.Available = BooksVM.Book.Available;
                bookFromDb.Author = BooksVM.Book.Author;
                bookFromDb.CategoryId = BooksVM.Book.CategoryId;
                bookFromDb.PublisherId = BooksVM.Book.PublisherId;
                
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(BooksVM);
        }

        //Get : Details
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BooksVM.Book = await _db.Books.Include(m => m.Categories).Include(m => m.Publisher).SingleOrDefaultAsync(m => m.Id == id);

            if (BooksVM.Book == null)
            {
                return NotFound();
            }

            return View(BooksVM);
        }

        //Get : Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BooksVM.Book = await _db.Books.Include(m => m.Categories).Include(m => m.Publisher).SingleOrDefaultAsync(m => m.Id == id);

            if (BooksVM.Book == null)
            {
                return NotFound();
            }

            return View(BooksVM);
        }

        // Post : Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Book book = await _db.Books.FindAsync(id);

            if(book == null)
            {
                return NotFound();
            }    
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(book.Image);
                
                if(System.IO.File.Exists(Path.Combine(uploads,book.Id+extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, book.Id + extension));
                }
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }    
        }

    }
}
