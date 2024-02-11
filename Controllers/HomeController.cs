using DeckMaster.Data;
using DeckMaster.Models;
using DeckMaster.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Diagnostics;
using System.Web;

namespace DeckMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly MyRegisteredUserRepo _myRegisteredUserRepo;
        private readonly IConfiguration _configuration;


        public HomeController(ILogger<HomeController> logger, 
                              ApplicationDbContext context,
                              MyRegisteredUserRepo myRegisteredUserRepo,
                              IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _myRegisteredUserRepo = myRegisteredUserRepo;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Get the current user's email
            string userEmail = User.Identity.Name;
            // Get and store the user's first name in session
            string firstName = _myRegisteredUserRepo.GetFirstNameByEmail(HttpUtility.HtmlEncode(userEmail)); // Encoded user input
            if (firstName != null)
            {
                HttpContext.Session.SetString("FirstName", firstName);
            }

            var pepper = _configuration["pepper"];
            var connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            return View("Index", "3.55|CAD");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult SecureArea()
        {
            string userName = User.Identity.Name;
            var registeredUser = _context.MyRegisteredUsers.FirstOrDefault(ru => ru.Email == userName);

            return View(registeredUser);
        }
    }
}
