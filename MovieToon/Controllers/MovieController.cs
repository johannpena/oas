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
    [Authorize(Roles = "Administrator")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.MovieCategory).Include(m => m.MovieClasification);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movies
                .Include(m => m.MovieCategory)
                .Include(m => m.MovieClasification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            ViewData["MovieCategoryId"] = new SelectList(_context.MovieCategories, "Id", "Name");
            ViewData["MovieClasificationId"] = new SelectList(_context.MovieClasifications, "Id", "Name");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,MovieClasificationId,MovieCategoryId")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieCategoryId"] = new SelectList(_context.MovieCategories, "Id", "Name", movieModel.MovieCategoryId);
            ViewData["MovieClasificationId"] = new SelectList(_context.MovieClasifications, "Id", "Name", movieModel.MovieClasificationId);
            return View(movieModel);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movies.FindAsync(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            ViewData["MovieCategoryId"] = new SelectList(_context.MovieCategories, "Id", "Name", movieModel.MovieCategoryId);
            ViewData["MovieClasificationId"] = new SelectList(_context.MovieClasifications, "Id", "Name", movieModel.MovieClasificationId);
            return View(movieModel);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,MovieClasificationId,MovieCategoryId")] MovieModel movieModel)
        {
            if (id != movieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelExists(movieModel.Id))
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
            ViewData["MovieCategoryId"] = new SelectList(_context.MovieCategories, "Id", "Name", movieModel.MovieCategoryId);
            ViewData["MovieClasificationId"] = new SelectList(_context.MovieClasifications, "Id", "Name", movieModel.MovieClasificationId);
            return View(movieModel);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movies
                .Include(m => m.MovieCategory)
                .Include(m => m.MovieClasification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieModel = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
