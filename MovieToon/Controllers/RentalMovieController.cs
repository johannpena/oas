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
    [Authorize(Roles = "Administrator,Employee")]
    public class RentalMovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalMovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RentalMovie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RentalMovies.Include(r => r.Movie).Include(r => r.Rental);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RentalMovie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalMovieModel = await _context.RentalMovies
                .Include(r => r.Movie)
                .Include(r => r.Rental)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalMovieModel == null)
            {
                return NotFound();
            }

            return View(rentalMovieModel);
        }

        // GET: RentalMovie/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id");
            return View();
        }

        // POST: RentalMovie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RentalId,MovieId,Price")] RentalMovieModel rentalMovieModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentalMovieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentalMovieModel.MovieId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id", rentalMovieModel.RentalId);
            return View(rentalMovieModel);
        }

        // GET: RentalMovie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalMovieModel = await _context.RentalMovies.FindAsync(id);
            if (rentalMovieModel == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentalMovieModel.MovieId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id", rentalMovieModel.RentalId);
            return View(rentalMovieModel);
        }

        // POST: RentalMovie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RentalId,MovieId,Price")] RentalMovieModel rentalMovieModel)
        {
            if (id != rentalMovieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalMovieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalMovieModelExists(rentalMovieModel.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", rentalMovieModel.MovieId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id", rentalMovieModel.RentalId);
            return View(rentalMovieModel);
        }

        // GET: RentalMovie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalMovieModel = await _context.RentalMovies
                .Include(r => r.Movie)
                .Include(r => r.Rental)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalMovieModel == null)
            {
                return NotFound();
            }

            return View(rentalMovieModel);
        }

        // POST: RentalMovie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalMovieModel = await _context.RentalMovies.FindAsync(id);
            _context.RentalMovies.Remove(rentalMovieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalMovieModelExists(int id)
        {
            return _context.RentalMovies.Any(e => e.Id == id);
        }
    }
}
