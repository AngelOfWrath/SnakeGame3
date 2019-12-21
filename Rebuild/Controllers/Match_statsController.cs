using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rebuild.Data;
using Rebuild.Models.Mathes_stats;

namespace Rebuild.Controllers
{
    public class Match_statsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Match_statsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Match_stats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Match_stats.ToListAsync());
        }

        // GET: Match_stats/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match_stats = await _context.Match_stats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match_stats == null)
            {
                return NotFound();
            }

            return View(match_stats);
        }

        // GET: Match_stats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Match_stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,value")] Match_stats match_stats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match_stats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match_stats);
        }

        // GET: Match_stats/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match_stats = await _context.Match_stats.FindAsync(id);
            if (match_stats == null)
            {
                return NotFound();
            }
            return View(match_stats);
        }

        // POST: Match_stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,value")] Match_stats match_stats)
        {
            if (id != match_stats.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match_stats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Match_statsExists(match_stats.Id))
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
            return View(match_stats);
        }

        // GET: Match_stats/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match_stats = await _context.Match_stats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match_stats == null)
            {
                return NotFound();
            }

            return View(match_stats);
        }

        // POST: Match_stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var match_stats = await _context.Match_stats.FindAsync(id);
            _context.Match_stats.Remove(match_stats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Match_statsExists(long id)
        {
            return _context.Match_stats.Any(e => e.Id == id);
        }
    }
}
