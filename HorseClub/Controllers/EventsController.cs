using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseClub.Data;
using HorseClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseClub.Controllers
{
    namespace HorseClub.Controllers
    {
        public class EventsController : Controller
        {
            private readonly HorseClubContext _context;

            public EventsController(HorseClubContext context)
            {
                _context = context;
            }

            // GET: Events
            public async Task<IActionResult> Index()
            {                
                
                  return View(await _context.Events.ToListAsync());                   
            }

            // GET: Events/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Events = await _context.Events
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Events == null)
                {
                    return NotFound();
                }

                return View(Events);
            }

            public IActionResult Display()
            {
                return View();
            }

            // GET: Events/Create
            public IActionResult Create()
            {
                return View();
            }
            
            // POST: Events/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,EventGroup,Time,Location,Message")] Events Events)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Events);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Display));
                }
                return View(Events);
            }

            // GET: Events/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Events = await _context.Events.FindAsync(id);
                if (Events == null)
                {
                    return NotFound();
                }
                return View(Events);
            }

            // POST: Events/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EventGroup,Time,Location,Message")] Events Events)
            {
                if (id != Events.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Events);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventsExists(Events.Id))
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
                return View(Events);
            }

            // GET: Events/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Events = await _context.Events
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Events == null)
                {
                    return NotFound();
                }

                return View(Events);
            }

            // POST: Events/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Events = await _context.Events.FindAsync(id);
                _context.Events.Remove(Events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool EventsExists(int id)
            {
                return _context.Events.Any(e => e.Id == id);
            }
        }
    }
}
