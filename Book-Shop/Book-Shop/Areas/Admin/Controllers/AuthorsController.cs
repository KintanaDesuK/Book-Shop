using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthorsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Authors.ToList());
        }
        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Add(author);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
