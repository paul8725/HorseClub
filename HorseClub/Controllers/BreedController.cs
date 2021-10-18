using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseClub.Data;
using HorseClub.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HorseClub.Controllers
{
    public class BreedController : Controller
    {
        private readonly HorseClubContext _context;

        public BreedController(HorseClubContext context)
        {
            _context = context;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
          return View(await _context.Breeds.ToListAsync());
        }
        public async Task<IActionResult> AddBreed(int? id)
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                YourHorses buyBook = new YourHorses();
                var book = _context.Breeds.Where(x => x.Id == id).FirstOrDefault();
                buyBook.Breed = book.Breed;
                buyBook.Name = book.Name;
                buyBook.Category = book.Category;
                buyBook.Price = book.Price;
                buyBook.UserId = Convert.ToInt32(value);
                _context.Add(buyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserIndex", "Breeds");
            }
            else
                return RedirectToAction("UserLogin", "Login");
        }

    }
}
