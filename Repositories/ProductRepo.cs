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
            return _context.Products.Select(r => new Product 
                            { 
                                ID = r.ID, 
                                ProductName = r.ProductName,
                                Description = r.Description,
                                Price = r.Price,
                                Currency = r.Currency,
                                ImageName = r.ImageName
                            });
        }
    }
}
