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
            // Get the current date and time as a string
            DateTime currentDateTime = DateTime.Now;
            string currentTime = currentDateTime.ToString("dd/MM/yyyy, HH:mm", System.Globalization.CultureInfo.GetCultureInfo("en-US"));

            // Get the email of the authenticated user
            string userEmail = HttpContext.User.Identity.Name;

            // Create a new IPN object and populate its properties
            IPN newIPN = new IPN
            {
                paymentID = IPN.paymentID,
                cart = IPN.cart,
                create_time = currentTime,
                payerID = IPN.payerID,
                payerFirstName = IPN.payerFirstName,
                payerLastName = IPN.payerLastName,
                payerMiddleName = IPN.payerMiddleName,
                payerEmail = userEmail,
                payerCountryCode = IPN.payerCountryCode,
                payerStatus = IPN.payerStatus,
                amount = IPN.amount,
                currency = "CAD",
                intent = IPN.intent,
                paymentMethod = "PayPal",
                paymentState = IPN.paymentState
            };

            // Save the IPN object to the database
            try
            {
                _context.IPNs.Add(newIPN);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                // For simplicity, just rethrow the exception for now
                throw ex;
            }

            // Return the view with the newly created IPN object
            return View(newIPN);
        }

    }
}
