using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTraq.Api.Models;

namespace BugTraq.Api.Controllers
{
    public class BugsController : Controller
    {
        private readonly BugTraqContext _context;

        public BugsController(BugTraqContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bugTraqContext = _context.Bugs.Include(b => b.User);
            return Ok(await bugTraqContext.ToListAsync());
        }

        // // GET: Bugs/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var bug = await _context.Bugs
        //         .Include(b => b.User)
        //         .FirstOrDefaultAsync(m => m.BugId == id);
        //     if (bug == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(bug);
        // }

        // // GET: Bugs/Create
        // public IActionResult Create()
        // {
        //     ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId");
        //     return View();
        // }

        // // POST: Bugs/Create
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("BugId,Title,Description,CreatedDate,Status,UserId")] Bug bug)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(bug);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", bug.UserId);
        //     return View(bug);
        // }

        // // GET: Bugs/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var bug = await _context.Bugs.FindAsync(id);
        //     if (bug == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", bug.UserId);
        //     return View(bug);
        // }

        // // POST: Bugs/Edit/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("BugId,Title,Description,CreatedDate,Status,UserId")] Bug bug)
        // {
        //     if (id != bug.BugId)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(bug);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!BugExists(bug.BugId))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "UserId", bug.UserId);
        //     return View(bug);
        // }

        // // GET: Bugs/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var bug = await _context.Bugs
        //         .Include(b => b.User)
        //         .FirstOrDefaultAsync(m => m.BugId == id);
        //     if (bug == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(bug);
        // }

        // // POST: Bugs/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var bug = await _context.Bugs.FindAsync(id);
        //     _context.Bugs.Remove(bug);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool BugExists(int id)
        // {
        //     return _context.Bugs.Any(e => e.BugId == id);
        // }
    }
}
