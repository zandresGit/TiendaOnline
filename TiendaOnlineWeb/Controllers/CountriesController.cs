using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnlineWeb.Data;
using TiendaOnlineWeb.Models;

namespace TiendaOnlineWeb.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.countries.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid){
                try {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException) { 
                    if (dbUpdateException.InnerException.Message.Contains("duplicate")) { 
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre."); 
                    } 
                    else { 
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message); 
                    } 
                }
                catch (Exception exception) { 
                    ModelState.AddModelError(string.Empty, exception.Message); 
                }
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try { 
                    _context.Update(country); 
                    await _context.SaveChangesAsync(); 
                    return RedirectToAction(nameof(Index)); 
                }
                catch (DbUpdateException dbUpdateException) { 
                    if (dbUpdateException.InnerException.Message.Contains("duplicate")) { 
                        ModelState.AddModelError(string.Empty, "There are a record with the same name."); 
                    } 
                    else { 
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message); 
                    } 
                }
                catch (Exception exception) { 
                    ModelState.AddModelError(string.Empty, exception.Message); 
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null){
                return NotFound();
            }

            Country country = await _context.countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null){
                return NotFound();
            }

            _context.countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Countries/Delete/5
       
        private bool CountryExists(int id)
        {
            return _context.countries.Any(e => e.Id == id);
        }
    }
}
