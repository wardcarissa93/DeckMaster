using DeckMaster.Data;
using DeckMaster.Models;
using DeckMaster.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;

namespace DeckMaster.Controllers
{
    public class IPNController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IPNController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IPNRepo IPNRepo = new IPNRepo(_context);

            return View(IPNRepo.GetAllTransactions());
        }
    }
}