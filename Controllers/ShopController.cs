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
        private readonly IConfiguration _configuration;

        public ShopController(ApplicationDbContext context,
                              IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ProductRepo productRepo = new ProductRepo(_context);

            ViewData["ClientId"] = _configuration["PayPal:ClientId"];

            return View(productRepo.GetAllProducts());
        }

        public IActionResult IPN(IPN IPN)
        {
            return View(IPN);
        }
    }
}
