using DeckMaster.Data;
using DeckMaster.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeckMaster.Repositories
{
    public class IPNRepo
    {
        private readonly ApplicationDbContext _context;

        public IPNRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IPN> GetAllTransactions()
        {
            // Encapsulate data access logic within the repository
            return _context.IPNs.Select(t => new IPN
            {
                paymentID = t.paymentID,
                create_time = t.create_time,
                payerFirstName = t.payerFirstName,
                payerEmail = t.payerEmail,
                amount = t.amount,
                paymentMethod = t.paymentMethod
            }).ToList(); // Execute the query immediately to prevent delayed execution with potential SQL injection
        }
    }
}
