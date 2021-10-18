using HorseClub.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorseClub.Models;

namespace HorseClub.Controllers
{
    public class YourHorsesController : Controller
    {
        private readonly HorseClubContext _context;

        public YourHorsesController(HorseClubContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                return View(await _context.YourHorses.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
            }
            else
                return RedirectToAction("UserLogin", "Login");
        }


        // GET: YourHorses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var YourHorses = await _context.YourHorses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (YourHorses == null)
            {
                return NotFound();
            }

            return View(YourHorses);
        }
        public IActionResult NoBreed()
        {
            return View();
        }

        // GET: YourHorses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YourHorses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Breed,Name,Category,Price")] YourHorses YourHorses)
        {
            if (ModelState.IsValid)
            {
                //YourHorses.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                _context.Add(YourHorses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(YourHorses);
        }

        // GET: YourHorses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var YourHorses = await _context.YourHorses.FindAsync(id);
            if (YourHorses == null)
            {
                return NotFound();
            }
            return View(YourHorses);
        }

        // POST: YourHorses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Breed,Name,Category,Price")] YourHorses YourHorses)
        {
            if (id != YourHorses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Breed = _context.YourHorses.FirstOrDefault(x => x.Id == YourHorses.Id);
                    Breed.Id = YourHorses.Id;
                    Breed.Breed = YourHorses.Breed;
                    Breed.Price = YourHorses.Price;
                    Breed.Name = YourHorses.Name;
                    Breed.Category = YourHorses.Category;
                    _context.YourHorses.Update(Breed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YourHorsesExists(YourHorses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(YourHorses);
        }

        // GET: YourHorses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var YourHorses = await _context.YourHorses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (YourHorses == null)
            {
                return NotFound();
            }

            return View(YourHorses);
        }

        // POST: YourHorses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var YourHorses = await _context.YourHorses.FindAsync(id);
            _context.YourHorses.Remove(YourHorses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YourHorsesExists(int id)
        {
            return _context.YourHorses.Any(e => e.Id == id);
        }



    }
}
