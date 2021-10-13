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
    public class MovieCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieCategories.ToListAsync());
        }

        // GET: MovieCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCategoryModel = await _context.MovieCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieCategoryModel == null)
            {
                return NotFound();
            }

            return View(movieCategoryModel);
        }

        // GET: MovieCategory/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] MovieCategoryModel movieCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieCategoryModel);
        }

        // GET: MovieCategory/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCategoryModel = await _context.MovieCategories.FindAsync(id);
            if (movieCategoryModel == null)
            {
                return NotFound();
            }
            return View(movieCategoryModel);
        }

        // POST: MovieCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MovieCategoryModel movieCategoryModel)
        {
            if (id != movieCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieCategoryModelExists(movieCategoryModel.Id))
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
            return View(movieCategoryModel);
        }

        // GET: MovieCategory/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCategoryModel = await _context.MovieCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieCategoryModel == null)
            {
                return NotFound();
            }

            return View(movieCategoryModel);
        }

        // POST: MovieCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieCategoryModel = await _context.MovieCategories.FindAsync(id);
            _context.MovieCategories.Remove(movieCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieCategoryModelExists(int id)
        {
            return _context.MovieCategories.Any(e => e.Id == id);
        }
    }
}
