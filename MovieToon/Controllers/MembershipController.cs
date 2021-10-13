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
    public class MembershipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Membership
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memberships.ToListAsync());
        }

        // GET: Membership/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _context.Memberships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipModel == null)
            {
                return NotFound();
            }

            return View(membershipModel);
        }

        // GET: Membership/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Discount")] MembershipModel membershipModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipModel);
        }

        // GET: Membership/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _context.Memberships.FindAsync(id);
            if (membershipModel == null)
            {
                return NotFound();
            }
            return View(membershipModel);
        }

        // POST: Membership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Discount")] MembershipModel membershipModel)
        {
            if (id != membershipModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipModelExists(membershipModel.Id))
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
            return View(membershipModel);
        }

        // GET: Membership/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _context.Memberships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipModel == null)
            {
                return NotFound();
            }

            return View(membershipModel);
        }

        // POST: Membership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipModel = await _context.Memberships.FindAsync(id);
            _context.Memberships.Remove(membershipModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipModelExists(int id)
        {
            return _context.Memberships.Any(e => e.Id == id);
        }
    }
}
