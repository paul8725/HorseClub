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
        public class BreedsController : Controller
        {
            private readonly HorseClubContext _context;

            public BreedsController(HorseClubContext context)
            {
                _context = context;
            }

            // GET: Breeds
            public async Task<IActionResult> Index()
            {
            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("LoginUser") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("LoginUser");
                var Login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                if (name == "HorseClub@admin.com")
                {
                    return View(await _context.Breeds.ToListAsync());
                    }
                    else
                    {
                    if (_context.Breeds.ToList().Count == 0)
                    {
                        return RedirectToAction(nameof(NoBreed));
                    }
                    else
                    return RedirectToAction("Index", "Breed");
                    }
            }
            else
                return RedirectToAction("UserLogin", "Login");
            }

        public async Task<IActionResult> UserIndex()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                return View(await _context.YourHorses.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
            }
            else
                return RedirectToAction("UserLogin", "Login");
        }
        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Breeds = await _context.Breeds
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Breeds == null)
                {
                    return NotFound();
                }

                return View(Breeds);
            }
        public IActionResult NoBreed()
        {
            return View();
        }
        
        // GET: Breeds/Create
        public IActionResult Create()
            {
            return View();
            }

            // POST: Breeds/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Breed,Name,Category,Price")] Breeds Breeds)
            {
                if (ModelState.IsValid)
                {
                    //Breeds.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(Breeds);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Breeds);
            }

            // GET: Breeds/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Breeds = await _context.Breeds.FindAsync(id);
                if (Breeds == null)
                {
                    return NotFound();
                }
                return View(Breeds);
            }

            // POST: Breeds/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Breed,Name,Category,Price")] Breeds Breeds)
            {
                if (id != Breeds.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Breeds);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BreedsExists(Breeds.Id))
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
                return View(Breeds);
            }

            // GET: Breeds/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Breeds = await _context.Breeds
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Breeds == null)
                {
                    return NotFound();
                }

                return View(Breeds);
            }

            // POST: Breeds/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Breeds = await _context.Breeds.FindAsync(id);
                _context.Breeds.Remove(Breeds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BreedsExists(int id)
            {
                return _context.Breeds.Any(e => e.Id == id);
            }
        }
    }
