using DeckMaster.Data;
using DeckMaster.Models;
using System.Web;

namespace DeckMaster.Repositories
{
    public class MyRegisteredUserRepo
    {
        private readonly ApplicationDbContext _context;

        public MyRegisteredUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMyRegisteredUser(MyRegisteredUser user)
        {
            _context.MyRegisteredUsers.Add(user);
            _context.SaveChanges();
        }

        public string GetUserFullNameByEmail(string email)
        {
            var user = _context.MyRegisteredUsers
                               .Where(u => u.Email == email)
                               .FirstOrDefault();

            return HttpUtility.HtmlEncode(user?.FirstName + " " + user?.LastName); // Encoded output
        }

        // Get the first name of a client by email
        public string GetFirstNameByEmail(string email)
        {
            var user = _context.MyRegisteredUsers.FirstOrDefault(u => u.Email == email);
            return HttpUtility.HtmlEncode(user?.FirstName); // Encoded output
        }
    }
}
