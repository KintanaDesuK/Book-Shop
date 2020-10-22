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
    public class PublishersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PublishersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Publishers.ToList());
        }
        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _db.Add(publisher);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }


        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(publisher);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }


        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _db.Publishers.FindAsync(id);
            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
