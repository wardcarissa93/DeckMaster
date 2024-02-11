using DeckMaster.Data;
using DeckMaster.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeckMaster.Repositories
{
    public class ProductRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            // Encapsulate data access logic within the repository
            return _context.Products.ToList(); // Execute the query immediately to prevent delayed execution with potential SQL injection
        }
    }
}
