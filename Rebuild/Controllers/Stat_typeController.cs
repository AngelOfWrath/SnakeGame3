using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rebuild.Data;
using Rebuild.Models.Stat_types;

namespace Rebuild.Controllers
{
    public class Stat_typeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Stat_typeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stat_type
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stat_types.ToListAsync());
        }

        // GET: Stat_type/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat_type = await _context.Stat_types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stat_type == null)
            {
                return NotFound();
            }

            return View(stat_type);
        }

        // GET: Stat_type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stat_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stat_Name")] Stat_type stat_type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stat_type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stat_type);
        }

        // GET: Stat_type/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat_type = await _context.Stat_types.FindAsync(id);
            if (stat_type == null)
            {
                return NotFound();
            }
            return View(stat_type);
        }

        // POST: Stat_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Stat_Name")] Stat_type stat_type)
        {
            if (id != stat_type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stat_type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Stat_typeExists(stat_type.Id))
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
            return View(stat_type);
        }

        // GET: Stat_type/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat_type = await _context.Stat_types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stat_type == null)
            {
                return NotFound();
            }

            return View(stat_type);
        }

        // POST: Stat_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var stat_type = await _context.Stat_types.FindAsync(id);
            _context.Stat_types.Remove(stat_type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Stat_typeExists(long id)
        {
            return _context.Stat_types.Any(e => e.Id == id);
        }
    }
}
