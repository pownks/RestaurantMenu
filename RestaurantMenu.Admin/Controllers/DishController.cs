using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Admin.Data;
using RestaurantMenu.Admin.ViewModels;
using RestaurantMenu.Admin.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantMenu.Admin.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        private readonly RestaurantDbContext _context;

        public DishController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Dish
        public async Task<IActionResult> Index()
        {
            var restaurantDbContext = _context.Dishes.Include(d => d.DishCategory);
            return View(await restaurantDbContext.ToListAsync());
        }

        // GET: Dish/Create
        public IActionResult Create()
        {
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name");
            return View();
        }

        // POST: Dish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishViewModel model, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("image", "Файл не обрано");
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    model.Dish.ImageBase64 = Convert.ToBase64String(fileBytes);
                }
            }
            
            if (!ModelState.IsValid)
            {
                ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", model.Dish.DishCategoryId);
                return View(model);
            }

            var dish = new Dish
            {
                Name = model.Dish.Name,
                Consist = model.Dish.Consist,
                DishCategoryId = model.Dish.DishCategoryId,
                Weight = model.Dish.Weight,
                Price = model.Dish.Price,
                ImageBase64 = model.Dish.ImageBase64,
                SpecialPrice = model.Dish.SpecialPrice,
                Special = model.Dish.Special
            };
            _context.Add(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dishes == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", dish.DishCategoryId);
            DishViewModel model = new DishViewModel
            {
                Dish = dish
            };
            return View(model);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DishViewModel model, IFormFile? image)
        {
            if (id != model.Dish.Id)
            {
                return NotFound();
            }

            if (image != null && image.Length != 0)
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    model.Dish.ImageBase64 = Convert.ToBase64String(fileBytes);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(model.Dish.Id))
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
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", model.Dish.DishCategoryId);
            return View(model);
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dishes == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dishes == null)
            {
                return Problem("Entity set 'RestaurantDbContext.dishes'  is null.");
            }
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
          return (_context.Dishes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
