﻿using DeckMaster.Data;
using DeckMaster.Models;

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

            return user?.FirstName + " " + user?.LastName;
        }
    }
}