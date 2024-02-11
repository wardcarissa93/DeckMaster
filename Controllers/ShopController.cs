using DeckMaster.Data;
using DeckMaster.Models;
using DeckMaster.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Added
using System;

namespace DeckMaster.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShopController> _logger;

        public ShopController(ApplicationDbContext context,
                              IConfiguration configuration,
                              ILogger<ShopController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ProductRepo productRepo = new ProductRepo(_context);

            ViewData["ClientId"] = _configuration["PayPal:ClientId"];

            return View(productRepo.GetAllProducts());
        }

        public IActionResult IPN(IPN IPN)
        {
            // Validate model state to prevent overposting attacks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                payerEmail = userEmail, // Assign user email directly
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
                // Log the exception and return an error response
                _logger.LogError(ex, "Failed to save IPN to the database.");
                return StatusCode(500, "Internal Server Error");
            }

            // Return the view with the newly created IPN object
            return View(newIPN);
        }
    }
}
