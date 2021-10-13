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
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals
                .Include(c => c.Customer).ThenInclude(cu => cu.Membership)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.Rentals
                .Include(c => c.Customer).ThenInclude(cu => cu.Membership)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie).ThenInclude(mv => mv.MovieClasification)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie).ThenInclude(mv => mv.MovieCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalModel == null)
            {
                return NotFound();
            }

            return View(rentalModel);
        }

        // GET: Rental/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["Movies"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,StartDate,EndDate,Movies")] RentalModel rentalModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentalModel);
                await _context.SaveChangesAsync();

                rentalModel = await _context.Rentals.Include(c => c.Customer).FirstOrDefaultAsync(m => m.Id == rentalModel.Id);
                CustomerModel customerModel = await _context.Customers.Include(c => c.Membership).FirstOrDefaultAsync(m => m.Id == rentalModel.CustomerId);
                foreach (int movieId in rentalModel.Movies)
                {
                    var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                    var rentalMovie = new RentalMovieModel { MovieId = movie.Id, RentalId = rentalModel.Id, Price = (movie.Price * (1 - customerModel.Membership.Discount)) };
                    _context.RentalMovies.Add(rentalMovie);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", rentalModel.CustomerId);
            return View(rentalModel);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.Rentals
                .Include(c => c.Customer).ThenInclude(cu => cu.Membership)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie).ThenInclude(mv => mv.MovieClasification)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie).ThenInclude(mv => mv.MovieCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalModel == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", rentalModel.CustomerId);
            return View(rentalModel);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,StartDate,EndDate")] RentalModel rentalModel)
        {
            if (id != rentalModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalModelExists(rentalModel.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", rentalModel.CustomerId);
            return View(rentalModel);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.Rentals
                .Include(c => c.Customer).ThenInclude(cu => cu.Membership)
                .Include(c => c.RentalMovies).ThenInclude(rm => rm.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentalModel == null)
            {
                return NotFound();
            }

            return View(rentalModel);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalModel = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rentalModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalModelExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
