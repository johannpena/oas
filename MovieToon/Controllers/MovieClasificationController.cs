using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MovieToon.Data;
using MovieToon.Models;

namespace MovieToon.Controllers
{
    [Authorize]
    public class MovieClasificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieClasificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieClasification
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieClasifications.ToListAsync());
        }

        // GET: MovieClasification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieClasificationModel = await _context.MovieClasifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieClasificationModel == null)
            {
                return NotFound();
            }

            return View(movieClasificationModel);
        }

        // GET: MovieClasification/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieClasification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] MovieClasificationModel movieClasificationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieClasificationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieClasificationModel);
        }

        // GET: MovieClasification/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieClasificationModel = await _context.MovieClasifications.FindAsync(id);
            if (movieClasificationModel == null)
            {
                return NotFound();
            }
            return View(movieClasificationModel);
        }

        // POST: MovieClasification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MovieClasificationModel movieClasificationModel)
        {
            if (id != movieClasificationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieClasificationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieClasificationModelExists(movieClasificationModel.Id))
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
            return View(movieClasificationModel);
        }

        // GET: MovieClasification/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieClasificationModel = await _context.MovieClasifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieClasificationModel == null)
            {
                return NotFound();
            }

            return View(movieClasificationModel);
        }

        // POST: MovieClasification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieClasificationModel = await _context.MovieClasifications.FindAsync(id);
            _context.MovieClasifications.Remove(movieClasificationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieClasificationModelExists(int id)
        {
            return _context.MovieClasifications.Any(e => e.Id == id);
        }
    }
}
