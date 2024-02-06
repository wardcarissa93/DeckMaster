using DeckMaster.Data;
using DeckMaster.Models;
using DeckMaster.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;

namespace DeckMaster.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ProductRepo productRepo = new ProductRepo(_context);

            return View(productRepo.GetAllProducts());
        }
    }
}
