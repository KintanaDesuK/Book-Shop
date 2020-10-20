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
            return View();
        }
    }
}
