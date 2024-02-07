using DeckMaster.Data;
using DeckMaster.Models;

namespace DeckMaster.Repositories
{
    public class ProductRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Select(p => new Product 
                            { 
                                ID = p.ID, 
                                ProductName = p.ProductName,
                                Description = p.Description,
                                Price = p.Price,
                                Currency = p.Currency,
                                ImageName = p.ImageName
                            });
        }
    }
}
