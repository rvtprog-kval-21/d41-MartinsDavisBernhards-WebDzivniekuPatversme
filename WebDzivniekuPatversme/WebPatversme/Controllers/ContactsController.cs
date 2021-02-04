﻿using System.Diagnostics;
using WebDzivniekuPatversme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebDzivniekuPatversme.Services.Interfaces;

namespace WebDzivniekuPatversme.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsServices;

        public ContactsController(
            IContactsService contactsServices)
        {
            _contactsServices = contactsServices;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}