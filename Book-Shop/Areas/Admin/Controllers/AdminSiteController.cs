using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class AdminSiteController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminSiteController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //StatisticalViewModel board = new StatisticalViewModel();
            //board.Products_Count = _db.Products.Count();
            //board.Customers_Count = _db.ApplicationUser.Where(m => m.Role == SD.Customer).Count();
            //board.Orders_Count = _db.Orders.Count();
            //board.Vendors_Count = _db.Vendors.Count();
            //board.Brands_Count = _db.Brands.Count();
            return View();
        }
    }
}