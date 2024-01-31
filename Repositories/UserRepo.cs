﻿using DeckMaster.Data;
using DeckMaster.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DeckMaster.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<UserVM> GetAllUsers()
        {
            IEnumerable<UserVM> users = _context.Users.Select(u => new UserVM { Email = u.Email });

            return users;
        }

        public SelectList GetUserSelectList()
        {
            IEnumerable<SelectListItem> users = GetAllUsers().Select(u => new SelectListItem
                                                                    {
                                                                        Value = u.Email,
                                                                        Text = u.Email
                                                                    });

            SelectList roleSelectList = new SelectList(users, "Value", "Text");


            return roleSelectList;
        }
    }
}
