using DeckMaster.Data;
using DeckMaster.Models;

namespace DeckMaster.Repositories
{
    public class IPNRepo
    {
        private readonly ApplicationDbContext _context;

        public IPNRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<IPN> GetAllTransactions()
        {
            return _context.IPNs.Select(t => new IPN
            {
                paymentID = t.paymentID,
                create_time = t.create_time,
                payerFirstName = t.payerFirstName,
                payerEmail = t.payerEmail,
                amount = t.amount,
                paymentMethod = t.paymentMethod
            });
        }
    }
}