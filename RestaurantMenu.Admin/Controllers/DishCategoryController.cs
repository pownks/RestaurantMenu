﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Admin.Data;
using RestaurantMenu.Admin.Models;

namespace RestaurantMenu.Admin.Controllers
{
    [Authorize]
    public class DishCategoryController : Controller
    {
        private readonly RestaurantDbContext _context;

        public DishCategoryController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: DishCategory
        public async Task<IActionResult> Index()
        {
              return _context.DishCategories != null ? 
                          View(await _context.DishCategories.ToListAsync()) :
                          Problem("Entity set 'RestaurantDbContext.DishCategories'  is null.");
        }

        // GET: DishCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategory);
        }

        // GET: DishCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DishCategories == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory == null)
            {
                return NotFound();
            }
            return View(dishCategory);
        }

        // POST: DishCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DishCategory dishCategory)
        {
            if (id != dishCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishCategoryExists(dishCategory.Id))
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
            return View(dishCategory);
        }

        // GET: DishCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DishCategories == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // POST: DishCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DishCategories == null)
            {
                return Problem("Entity set 'RestaurantDbContext.DishCategories'  is null.");
            }
            var dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory != null)
            {
                _context.DishCategories.Remove(dishCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishCategoryExists(int id)
        {
          return (_context.DishCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
